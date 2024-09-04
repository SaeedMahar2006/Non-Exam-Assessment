using NeaLibrary.Data.Other;
using NeaLibrary.DataStructures;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;

namespace NeaLibrary.Data
{

    public class DB_DataSet : IDataSet
    {
        private string sql_query;
        private string from_string;
        private string cols_string;
        public List<string> tables;
        public List<string> cols;
        public List<string> normalise_cols; private List<int> normalise_indexes;
        public List<string> relative_cols; private List<int> relative_indexes;
        public List<string> smooth_cols; private List<int> smooth_indexes;

        public List<string> ValueColumn; private List<int> ValueIndexes;

        public List<int> GetValueIndexes() => ValueIndexes;

        public double safety;

        private int smoother_steps;
        private int INTERNAL_DB_OFFSET = 30; //used to make sure no NULL values are encountered

        public int GetInternalDbOffset() => INTERNAL_DB_OFFSET;

        private SortedDictionary<int, (Vector, Vector)> cache = new SortedDictionary<int, (Vector, Vector)>();
        private object cacheLock = new object();

        private Dictionary<string, int> table_start = new Dictionary<string, int>();

        private Func<double, double> Normaliser = Tools.Tools.Sigmoid;
        private Func<IEnumerable<double>, double> Smoother = x => x.Sum() / x.Count(); // the smoother should return an average of the couple of values in the time series, first derivative doesnt really

        private int cached_size = -1;
        public InputMapCache InputMapCache;
        public void SetInputMapCache(InputMapCache inputMapCache)
        {
            this.InputMapCache = inputMapCache;
        }
        public InputMapCache GetInputMapCache()
        {
            return this.InputMapCache;
        }

        public double Compound { get; }

        public object driver_lock = new object();
        public SQL_Driver? driver;


        [field: NonSerialized]
        public event EventHandler<List<(Vector, Vector)>> ToListComplete;

        //(Vector, Vector) IDataSet.this[int n] { get => Batch(1, n).First() ; set => throw new NotImplementedException(); }
        (Vector, Vector) IDataSet.this[int n]
        {
            get
            {
                if (cache.ContainsKey(n)) return cache[n];
                return Batch(1, n).First();
            }
            set => throw new NotImplementedException();
        }

        private void InitialiseSQL()
        {
            //this.sql_query = sql_query;
            //            string path = System.AppContext.BaseDirectory;
            //#nullable enable
            //            path = Directory.GetParent(path).ToString();
            //            path = Directory.GetParent(path).ToString();

            //            path = Directory.GetParent(path).ToString();
            //            path = Directory.GetParent(path).ToString();
            //            path = Directory.GetParent(path).ToString();
            //#nullable disable
            if (driver != null) return;
            string path = Tools.Tools.GetGlobalVar("db_dir");
            lock (driver_lock) {
                driver = new SQL_Driver(path);
            }

        }

        private void ReCalcTableStart()
        {
            table_start = new Dictionary<string, int>();
            int pos = 0;
            tables.Sort(); // so would be deterministic in which tables come first. Allows for replication . is sort in place?
            foreach (string table in tables)
            {
                if (driver == null) throw new Exception("Database not initialised");
                table_start.Add(table, pos);
                SQLiteDataReader read = SQL_Driver.Query(driver.conn, $"SELECT Count(*) FROM {table}"); //No taking away the offset, this is CORRECT, bc iterator_one_table works so
                read.Read();
                //pos += read.GetInt32(0) + 1000; //wiggle room   In the very rare case should the DB get more values while DB_DataSet was still loaded so one table has more room than allocated?
                //cache could be overwritten if more data than wiggle room is added (approx 1000 days worth) and RandomBatch will give wrong result

                //wiggle room redundant. We are assuming data wont be added to database table while the DB_DataSet is open. once closed more can be added and upon reoppening recalctablestart will do its job
            }
        }


        public void Cache()
        {
            //generate wholy
            foreach (string table in table_start.Keys)
            {
                iterator_one_table(Count(), 0, table);
            }
        }
        public void ClearCache()
        {
            cache.Clear();
        }
        private IEnumerable<(Vector, Vector)> iterator_one_table(int size, int start, string table)  // size and start should be within this table
        {
            start += INTERNAL_DB_OFFSET;
            int real_item_index = start;
            bool Cached = true;


            ////LOCK HERE?  Read operation only so no need

            //    IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(driver.conn, /*"(" + from_string + ")"*/ table, cols, $"1=1 ORDER BY Date ASC LIMIT {size} OFFSET {start}");

            //    TrainingDataTransformer tr = new TrainingDataTransformer(input,     //MAYBE MOCE THESE OUTSIDE? SO NOT SPAWNED EVERYTIME
            //        relative_indexes,
            //        normalise_indexes,
            //        Normaliser,
            //        smooth_indexes,
            //        Smoother,
            //        smoother_steps
            //        );  //$"SELECT * FROM ({from_string}) LIMIT {size} OFFSET {start};")

            //    OutputDataProducer output = new OutputDataProducer(safety, 3, input, ValueIndexes, ValueIndexes.Count() / 2);   //TODO SAFETY
            //    IEnumerator<Vector> Tr = tr.GetEnumerator();
            //    IEnumerator<Vector> Output = output.GetEnumerator();
            //    for (int i = start; i < size + start; i++)
            //    {
            //        if (cache.ContainsKey(i))
            //        {
            //            yield return cache[i];
            //        }
            //        else
            //        {
            //            //IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(driver.conn, "(" + from_string + ")", cols, $"1=1 ORDER BY Date ASC LIMIT {size} OFFSET {start}");
            //            //TrainingDataTransformer tr = new TrainingDataTransformer(input,     //MAYBE MOCE THESE OUTSIDE? SO NOT SPAWNED EVERYTIME
            //            //    relative_indexes,
            //            //    normalise_indexes,
            //            //    Normaliser,
            //            //    smooth_indexes,
            //            //    Smoother,
            //            //    smoother_steps
            //            //    );  //$"SELECT * FROM ({from_string}) LIMIT {size} OFFSET {start};")

            //            //OutputDataProducer output = new OutputDataProducer(safety, 3, tr, ValueIndexes, ValueIndexes.Count() / 2);   //TODO SAFETY
            //            //IEnumerator<Vector> Tr = tr.GetEnumerator();
            //            //IEnumerator<Vector> Output = output.GetEnumerator();
            //            //while (Tr.MoveNext() && Output.MoveNext())
            //            //{

            //            try{
            //                Tr.MoveNext(); Output.MoveNext();
            //            }
            //            catch { break; }

            //            if (!cache.ContainsKey(table_start[table] + i )) { cache.Add(table_start[table] + i, (Tr.Current, Output.Current)); }
            //            yield return (Tr.Current, Output.Current);
            //            real_item_index++;
            //            //}

            //            //break; //brak the loop
            //        }
            //    }

            //    yield break;


            IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(driver.conn, /*"(" + from_string + ")"*/ table, cols, $"1=1 ORDER BY Date ASC LIMIT {size} OFFSET {start}");
            //IEnumerable<string> d = SQL_Driver.ReadColumn<string>(driver.conn, /*"(" + from_string + ")"*/ table, "Date", $"1=1 ORDER BY Date ASC LIMIT {size} OFFSET {start}");
            /*IEnumerator<string> D = d.GetEnumerator();*/ IEnumerator<Vector> Input = input.GetEnumerator();
            TrainingDataTransformer data_transformer = new TrainingDataTransformer(input,     //MAYBE MOCE THESE OUTSIDE? SO NOT INITIATED EVERYTIME
    relative_indexes,
    normalise_indexes,
    Normaliser,
    smooth_indexes,
    Smoother,
    smoother_steps
    );  //$"SELECT * FROM ({from_string}) LIMIT {size} OFFSET {start};")

            OutputDataProducer output = new OutputDataProducer(safety, 3, input, ValueIndexes, ValueIndexes.Count() / 2);   //TODO SAFETY
            IEnumerator<Vector> TrainingDataTransformerEnumerator = data_transformer.GetEnumerator();
            IEnumerator<Vector> OutputEnumerator = output.GetEnumerator();
            for (int i = start; i < size + start; i++)
            {
                if (cache.ContainsKey(table_start[table] + i ))
                {
                    yield return cache[i];
                }
                //IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(driver.conn, "(" + from_string + ")", cols, $"1=1 ORDER BY Date ASC LIMIT {size} OFFSET {start}");
                //TrainingDataTransformer tr = new TrainingDataTransformer(input,     //MAYBE MOCE THESE OUTSIDE? SO NOT SPAWNED EVERYTIME
                //    relative_indexes,
                //    normalise_indexes,
                //    Normaliser,
                //    smooth_indexes,
                //    Smoother,
                //    smoother_steps
                //    );  //$"SELECT * FROM ({from_string}) LIMIT {size} OFFSET {start};")

                //OutputDataProducer output = new OutputDataProducer(safety, 3, tr, ValueIndexes, ValueIndexes.Count() / 2);   //TODO SAFETY
                //IEnumerator<Vector> Tr = tr.GetEnumerator();
                //IEnumerator<Vector> Output = output.GetEnumerator();
                //while (Tr.MoveNext() && Output.MoveNext())
                //{


                try
                {
                    TrainingDataTransformerEnumerator.MoveNext();
                    OutputEnumerator.MoveNext();
                }
                catch { break;/*ran out of elements to yield*/ }

                if (!cache.ContainsKey(table_start[table] + i)) {
                    lock (cacheLock)
                    {
                        if (!cache.ContainsKey(table_start[table] + i)) //a second check required as 2 threads could havereached this state waiting for lock realease to add same value to the cache
                        {
                            cache.Add(table_start[table] + i, (TrainingDataTransformerEnumerator.Current, OutputEnumerator.Current));
                        }
                        //sufficient as only 1 thread can have a lock at this time, so no race conditions, and no 2 threads can add the same item to add
                    }
                }
                yield return (TrainingDataTransformerEnumerator.Current, OutputEnumerator.Current);

                //}

                //break; //brak the loop

            }
            yield break;

        }

        public IEnumerable<(DateTime, Vector, Vector, Vector)> Debug(int size, int start, string table)
        {
            IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(driver.conn, /*"(" + from_string + ")"*/ table, cols, $"1=1 ORDER BY Date ASC LIMIT {size} OFFSET {start}");
            IEnumerable<string> d = SQL_Driver.ReadColumn<string>(driver.conn, /*"(" + from_string + ")"*/ table, "Date", $"1=1 ORDER BY Date ASC LIMIT {size} OFFSET {start}");
            IEnumerator<string> D = d.GetEnumerator(); IEnumerator<Vector> Input = input.GetEnumerator();
            TrainingDataTransformer tr = new TrainingDataTransformer(input,     //MAYBE MOCE THESE OUTSIDE? SO NOT SPAWNED EVERYTIME
    relative_indexes,
    normalise_indexes,
    Normaliser,
    smooth_indexes,
    Smoother,
    smoother_steps
    );  //$"SELECT * FROM ({from_string}) LIMIT {size} OFFSET {start};")

            OutputDataProducer output = new OutputDataProducer(safety, 3, input, ValueIndexes, ValueIndexes.Count() / 2);   //TODO SAFETY
            IEnumerator<Vector> Tr = tr.GetEnumerator();
            IEnumerator<Vector> Output = output.GetEnumerator();
            for (int i = start; i < size + start; i++)
            {

                    //IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(driver.conn, "(" + from_string + ")", cols, $"1=1 ORDER BY Date ASC LIMIT {size} OFFSET {start}");
                    //TrainingDataTransformer tr = new TrainingDataTransformer(input,     //MAYBE MOCE THESE OUTSIDE? SO NOT SPAWNED EVERYTIME
                    //    relative_indexes,
                    //    normalise_indexes,
                    //    Normaliser,
                    //    smooth_indexes,
                    //    Smoother,
                    //    smoother_steps
                    //    );  //$"SELECT * FROM ({from_string}) LIMIT {size} OFFSET {start};")

                    //OutputDataProducer output = new OutputDataProducer(safety, 3, tr, ValueIndexes, ValueIndexes.Count() / 2);   //TODO SAFETY
                    //IEnumerator<Vector> Tr = tr.GetEnumerator();
                    //IEnumerator<Vector> Output = output.GetEnumerator();
                    //while (Tr.MoveNext() && Output.MoveNext())
                    //{

                    try
                    {
                       D.MoveNext(); Input.MoveNext(); Tr.MoveNext(); Output.MoveNext();
                    }
                    catch { break; }

                    //if (!cache.ContainsKey(table_start[table] + i)) { cache.Add(table_start[table] + i, (Tr.Current, Output.Current)); }
                    yield return (DateTime.Parse( D.Current), Input.Current ,Tr.Current, Output.Current);

                    //}

                    //break; //brak the loop

            }
        }

        //public IEnumerable<(DateTime, (Vector,Vector))> GetAllWithDateTime()
        //{
        //    yield break;
        //} redundant

        //public IEnumerable<(Vector, Vector)> CACHE()
        //{
        //    int start = INTERNAL_DB_OFFSET;
        //    int index = start;
        //    if (driver == null) InitialiseSQL();
        //    IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(driver!.conn, "(" + from_string + ")", cols, $"1=1 ORDER BY Date ASC LIMIT -1 OFFSET {start};");
        //    TrainingDataTransformer tr = new TrainingDataTransformer(input,
        //        relative_indexes,
        //        normalise_indexes,
        //        Normaliser,
        //        smooth_indexes,
        //        Smoother,
        //        smoother_steps
        //    );  //$"SELECT * FROM ({from_string}) LIMIT {size} OFFSET {start};")

        //    OutputDataProducer output = new OutputDataProducer(safety, 3, input, ValueIndexes, ValueIndexes.Count() / 2);   //TODO SAFETY
        //    IEnumerator<Vector> Tr = tr.GetEnumerator();
        //    IEnumerator<Vector> Output = output.GetEnumerator();
        //    while (Tr.MoveNext() && Output.MoveNext())
        //    {
        //        if (!cache.ContainsKey(index)) { cache.Add(index, (Tr.Current, Output.Current)); }
        //        yield return (Tr.Current, Output.Current);
        //        index++;
        //    }

        //}
    

        /// <summary>
        /// start and size only include items that can be enumerated. Which means those that are past the DB OFFSET.
        /// This makes it the same as those accounted by .Count()
        /// </summary>
        /// <param name="size"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public IEnumerable<(Vector, Vector)> Batch(int size, int start)
        {

            List<string> tables_spanned = new List<string>();
            int yielded = 0;
            int items_count_from_tables = 0;
            foreach (KeyValuePair<string, int> v in table_start)
            {
                if (v.Value >= start) { tables_spanned.Add(v.Key); items_count_from_tables+=Count(v.Key); if (items_count_from_tables>=size) { break; } }//this includes subtracting DB OFFSET for each table
            }
            //bool complete=false;

            bool first_table=true;

            foreach (string table in tables_spanned) {//                     V determines the "virtual address"
                foreach((Vector,Vector) val in iterator_one_table(size, (first_table)? start - table_start[table] : 0 , table))
                    //if we wanna start a 1000 in, first table starts from 600, then we wanna start first table from 400
                    //ik i ignored db offset ->    accounted now. each table will start from 0 other than first
                    //all other tables after that are contiguous and thus start from 0. ik i need to fix the size requirement
                    //but it doesnt really matter in reality bc we have the "yielded" counter to stop us
                    //size >= (size of individual table) so this is ok too
                {
                    yield return val;
                    yielded++;
                    if (yielded >= size) break;
                }
                first_table = false;
                if (yielded >= size) break;
            }
        }

        /// <summary>
        /// SPOILER: doesnt acctually yield next batch. risk of thread safety was taken into consideration
        /// </summary>
        /// <param name="size"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public IEnumerable<(Vector, Vector)> NextBatch(int size, int start)
        {
            int returned = 0;

            foreach (var v in Batch(Count(), start)) //no need to men tion offset here it is accounted in the normal batch method
            {
                if (returned > size) yield break;
                yield return v;
                returned++;
            }
        }
        /// <summary>
        /// Only cached items can be selected. So I Cache() first
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<(Vector, Vector)> RandomBatch(int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (cache.Count == 0)
                {
                    yield return NextBatch(size, 0).ElementAt(Tools.Tools.RandomInt(0,size-1)); //as RandomInt is inclusive
                    continue;
                }
                //yield return (cache[Tools.Tools.RandomInt(0, Count() - 1)]); //-1 cuz 0 indexed and random inculsive
                yield return cache.ElementAt(Tools.Tools.RandomInt(0, cache.Count() - 1)).Value;
            }
            yield break;
            //yield return cache.ElementAt(Tools.Tools.RandomInt(0, cache.Count()-1)).Value;
        }
        public int Count()
        {
            if (cached_size != -1) return cached_size;
            //using (SQLiteDataReader r = SQL_Driver.Query(driver.conn, $"SELECT COUNT(*) FROM ({from_string});"))
            //{
            //    r.Read();

            //    cached_size = r.GetInt32(0) /*- INTERNAL_DB_OFFSET*/ - INTERNAL_DB_OFFSET*table_start.Count(); //Hm?
            //    return cached_size;
            //}
            int size = 0;
            foreach(string t in tables) { size += Count(t); }
            cached_size = size;
            return size;
        }
        public int Count(string table)
        {
            //if (cached_size != -1) return cached_size;
            using (SQLiteDataReader r = SQL_Driver.Query(driver.conn, $"SELECT COUNT(*) FROM ({table});"))
            {
                r.Read();
                //cached_size = r.GetInt32(0) - INTERNAL_DB_OFFSET;
                return r.GetInt32(0) - INTERNAL_DB_OFFSET;
            }
        }

        IEnumerator<(Vector, Vector)> IEnumerable<(Vector, Vector)>.GetEnumerator()
        {

            return Batch(Count(), 0).GetEnumerator();
            ////return IDataSet.NextBatch().GetEnumerator();

            //IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(driver.conn, "(" + from_string + ")", cols, $"1=1 ORDER BY Date ASC LIMIT -1 OFFSET {INTERNAL_DB_OFFSET}");
            //TrainingDataTransformer tr = new TrainingDataTransformer(input,
            //    relative_indexes,
            //    normalise_indexes,
            //    Normaliser,
            //    smooth_indexes,
            //    Smoother,
            //    smoother_steps
            //    );  //$"SELECT * FROM ({from_string}) LIMIT {size} OFFSET {start};")

            //OutputDataProducer output = new OutputDataProducer(safety, 3, input, ValueIndexes, ValueIndexes.Count()/2);
            //IEnumerator<Vector> Tr = tr.GetEnumerator();
            //IEnumerator<Vector> Output = output.GetEnumerator();
            //while (Tr.MoveNext() && Output.MoveNext())
            //{
            //    yield return (Tr.Current, Output.Current);
            //}
            //yield break;
        }

        /// <summary>
        /// Instatiate Db_dataset, specify parameters by name of columns. Value column advised to be only 1 string 
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="cols"></param>
        /// <param name="normalise_cols"></param>
        /// <param name="relative_cols"></param>
        /// <param name="smooth_cols"></param>
        /// <param name="value_cols"></param>
        /// <param name="safety"></param>
        /// <param name="smoother_steps"></param>
        /// <param name="cond"></param>
        /// <param name="offset"></param>
        public DB_DataSet(List<string> tables, List<string> cols, List<string> normalise_cols,
         List<string> relative_cols,
         List<string> smooth_cols,
         List<string> value_cols,
         double safety,
         int smoother_steps,
         string cond = "1=1",
         int offset = 30)
        {
            INTERNAL_DB_OFFSET = offset;
            from_string = "";
            foreach (string t in tables)
            {
                from_string += "SELECT * FROM ";
                from_string += t;
                from_string += " UNION ";
            }
            from_string = from_string.Substring(0, from_string.Length - 6);


            cols_string = from_string;
            
            this.tables = tables;
            this.cols = cols;
            sql_query = $"SELECT * FROM ({from_string}) WHERE {cond};";//in future work as desriptor, resolved with input map cache
            this.cols = cols;
            this.normalise_cols = normalise_cols;
            this.relative_cols = relative_cols;
            this.smooth_cols = smooth_cols;
            this.smoother_steps = smoother_steps;
            ValueColumn = value_cols;
            this.safety = safety;
            normalise_indexes = new List<int>();
            relative_indexes = new List<int>();
            smooth_indexes = new List<int>();
            ValueIndexes = new List<int>(); 

            Dictionary<int,string> for_inputmapcache= new Dictionary<int,string>();

            for (int i = 0; i < cols.Count; i++)
            {
                string col = cols[i];
                for_inputmapcache.Add(i, "");
                if (normalise_cols.Contains(col))
                {
                    normalise_indexes.Add(i);
                    for_inputmapcache[i] += "Normalised ";
                }
                if (relative_cols.Contains(col))
                {
                    relative_indexes.Add(i);
                    for_inputmapcache[i] += "Relativised ";
                }
                if (smooth_cols.Contains(col))
                {
                    smooth_indexes.Add(i);
                    for_inputmapcache[i] += "Smoothed ";
                }
                if (ValueColumn.Contains(col))
                {
                    for_inputmapcache[i] += "Value Column ";
                    ValueIndexes.Add(i);
                }
                for_inputmapcache[i] += $"{cols[i]}";
            }
            InputMapCache=new InputMapCache(cols.Count,for_inputmapcache,normalise_cols,relative_cols,value_cols.First());
            //InputMapCache.Normalise = normalise_cols;
            //InputMapCache.Relative = relative_cols;
            //InputMapCache.Value = value_cols.First();
            //InputMapCache.Normalise = normalise_cols;
            InitialiseSQL();
            ReCalcTableStart();
            //this = DB_DataSet(sql_query);
        }

        public void SetInputMapCacheDescription(int i, string description)
        {
            InputMapCache.InputDescription[i]= description;
        }

        public DB_DataSet(DB_DataSet_Context c)
        {
            sql_query = c.sql_query;
            from_string = c.from_string;
            cols_string = c.cols_string;
            tables = c.tables;
            cols=c.cols;
            normalise_cols = c.normalise_cols; normalise_indexes = c.normalise;
            relative_cols=c.relative_cols; relative_indexes = c.relative;
            smooth_cols=c.smooth_cols; smooth_indexes = c.smooth;
            ValueIndexes = c.ValueIndexes; ValueColumn=c.ValueColumn;
            smoother_steps=c.smoother_steps;
            INTERNAL_DB_OFFSET = c.INTERNAL_DB_OFFSET; //used to make sure no NULL values are encountered
            cache = c.cache;
            InputMapCache = c.InputMapCache;
            safety = c.safety;
            Compound = c.Compound;
            InitialiseSQL();
            ReCalcTableStart();
        }



        public NeaLibrary.DataStructures.DataSet ToDataSet()
        {
            NeaLibrary.DataStructures.DataSet ds = new NeaLibrary.DataStructures.DataSet();
            foreach ((Vector, Vector) data in Batch(Count(), 0))
            {
                ds.Add(data.Item1, data.Item2);
            }
            ds.SetInputMapCache(InputMapCache);
            return ds;
        }

        public List<(Vector,Vector)> ToList()
        {
            List<(Vector,Vector)> l=this.Batch(Count(), 0).ToList();
            ToListComplete(this, l);
            return l;
        }


        public void Save(string path)
        {
            DB_DataSet_Context context = new DB_DataSet_Context(
                sql_query,
                from_string,
                cols_string,
                tables,
                cols,
                normalise_cols, normalise_indexes,
                relative_cols, relative_indexes,
                smooth_cols, smooth_indexes,
                ValueColumn,ValueIndexes,
                safety,
                smoother_steps,
                INTERNAL_DB_OFFSET,
                cache,
                InputMapCache,
                Compound
                );
            Tools.Tools.Serialize(path, context);
        }
        public static IDataSet Load(string path)
        {
            //TODO null handling
            DB_DataSet_Context? n = null;
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            #pragma warning disable
            n = (DB_DataSet_Context)formatter.Deserialize(fs);
            fs.Close();
            return new DB_DataSet(n);
        }

    }



    [Serializable]
    public class DB_DataSet_Context
    {
        public string sql_query;
        public string from_string;
        public string cols_string;
        public List<string> tables;
        public List<string> cols;
        public List<string> normalise_cols; public List<int> normalise;
        public List<string> relative_cols; public List<int> relative;
        public List<string> smooth_cols; public List<int> smooth;
        public List<string> ValueColumn; public List<int> ValueIndexes;
        public double safety;
        public int smoother_steps;
        public int INTERNAL_DB_OFFSET = 30; //used to make sure no NULL values are encountered
        public SortedDictionary<int, (Vector, Vector)> cache = new SortedDictionary<int, (Vector, Vector)>();
        public InputMapCache InputMapCache;
        public double Compound;

        public DB_DataSet_Context(
            string sql_query,
            string from_string,
            string cols_string,
            List<string> tables,
            List<string> cols,
            List<string> normalise_cols, List<int> normalise,
            List<string> relative_cols, List<int> relative,
            List<string> smooth_cols, List<int> smooth,
            List<string> ValueColumn, List<int> ValueIndexes,
            double safety,
            int smoother_steps,
            int INTERNAL_DB_OFFSET, //used to make sure no NULL values are encountered
            SortedDictionary<int, (Vector, Vector)> cache,
            InputMapCache inputMapCache,
            double Compound

            )
        {
            this.sql_query = sql_query;
            this.from_string = from_string;
            this.cols_string = cols_string;
            this.tables = tables;
            this.cols = cols;
            this.normalise = normalise;
            this.normalise_cols = normalise_cols;
            this.relative_cols = relative_cols;
            this.relative = relative;
            this.smooth = smooth;
            this.smooth_cols = smooth_cols;
            this.smoother_steps = smoother_steps;
            this.INTERNAL_DB_OFFSET = INTERNAL_DB_OFFSET;
            this.cache = cache;
            this.ValueColumn = ValueColumn;
            this.ValueIndexes = ValueIndexes;
            this.safety = safety;
            this.InputMapCache=inputMapCache;
            this.Compound = Compound;
        }
    }
}
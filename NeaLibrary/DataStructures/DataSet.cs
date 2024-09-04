

using NeaLibrary.Data.Other;

using System.Runtime.Serialization.Formatters.Binary;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NeaLibrary.DataStructures
{
    [Serializable]
    public class DataSet : IDataSet
    {
        List<Vector> _data;
        List<Vector> _expected;
        int batch_counter = 0;

        public InputMapCache InputMapCache;
        public void SetInputMapCache(InputMapCache inputMapCache)
        {
            this.InputMapCache = inputMapCache;
        }
        public InputMapCache GetInputMapCache()
        {
            return this.InputMapCache;
        }
        public DataSet()
        {
            _data = new List<Vector>();
            _expected = new List<Vector>();
        }

        (Vector, Vector) IDataSet.this[int n]
        {
            get { return (_data[n], _expected[n]); }
            set { _data.Add(value.Item1); _expected.Add(value.Item2); }
        }

        public void Add(Vector data, Vector expected)
        {
            _data.Add(data);
            _expected.Add(expected);
        }


        /// <summary> 
        ///returns iterator of pairs of vectors, where each vector is a sample from the data and the expected value, respectively.
        /// </summary>
        /// <param name="size"></param>
        /// The number of elements in batch.
        /// <param name="start"></param>
        /// the index of the first element to include in the batch (default is 0).
        /// <returns></returns>
        public IEnumerable<(Vector, Vector)> Batch(int size, int start = 0)
        {
            for (int s = start; s < start + size; s++)
            {
                yield return (_data[s], _expected[s]);
            }
        }

        public int Count()
        {
            return _data.Count();
        }

        public IEnumerator<(Vector, Vector)> GetEnumerator()
        {
            for (int s = 0; s < Count(); s++) yield return (_data[s], _expected[s]);
        }

        public IEnumerable<(Vector, Vector)> NextBatch(int size, int start = -1)
        {
            if (start != -1)
            {
                batch_counter = start;
            }
            return Batch(size, batch_counter);
        }

        public void Save(string path)
        {
            Tools.Tools.Serialize(path, this);
        }
        public static IDataSet Load(string path)
        {
            //TODO null handling
            DataSet? n = null;
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            #pragma warning disable
            n = (DataSet)formatter.Deserialize(fs);
            fs.Close();
            return n;
        }

    }

    [Serializable]
    public class ArrayDataSet : IDataSet
    {
        public InputMapCache InputMapCache;
        public void SetInputMapCache(InputMapCache inputMapCache)
        {
            this.InputMapCache = inputMapCache;
        }
        public InputMapCache GetInputMapCache()
        {
            return this.InputMapCache;
        }
        Vector[] _data;
        Vector[] _expected;
        public ArrayDataSet(int size)
        {
            _data = new Vector[size];
            _expected = new Vector[size];
        }
        int batch_counter = 0;
        int count = 0;
        public void Add((Vector, Vector) val)
        {
            if (count < _data.Length)
            {
                _data[count] = val.Item1;
                _expected[count] = val.Item2;
                count++;
            }
        }

        (Vector, Vector) IDataSet.this[int n]
        {
            get { return (_data[n], _expected[n]); }
            set { Add(value); }
        }

        public IEnumerable<(Vector, Vector)> Batch(int size, int start = 0)
        {
            for (int s = start; s < start + size; s++)
            {
                yield return (_data[s], _expected[s]);
            }
        }

        public int Count()
        {
            return _data.Count();
        }

        public IEnumerator<(Vector, Vector)> GetEnumerator()
        {
            for (int s = 0; s < Count(); s++) yield return (_data[s], _expected[s]);
        }

        public IEnumerable<(Vector, Vector)> NextBatch(int size, int start = -1)
        {
            if (start != -1)
            {
                batch_counter = start;
            }
            return Batch(size, batch_counter);
        }
        public void Save(string path)
        {
            Tools.Tools.Serialize(path, this);
        }
        public static IDataSet Load(string path)
        {
            //TODO null handling
            ArrayDataSet? n = null;
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            #pragma warning disable
            n = (ArrayDataSet)formatter.Deserialize(fs);
            fs.Close();
            return n;
        }
    }
}
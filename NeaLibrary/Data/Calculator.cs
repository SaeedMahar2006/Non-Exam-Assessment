using Newtonsoft.Json.Linq;
using Skender.Stock.Indicators;
using System.Globalization;
using System.Numerics;


namespace NeaLibrary.Data
{


    public static class Calculator
    {
        private static SQL_Driver db = new SQL_Driver(Tools.Tools.GetGlobalVar("db_dir"));
        public static void CalculateRSI(string table, int n = 14)
        {
            using (IEnumerator<string> date = SQL_Driver.ReadColumn<string>(db.conn, table, "Date", "1=1 ORDER BY Date ASC").GetEnumerator())
            {
                using (IEnumerator<double> rsi = Tools.Tools.RSI(SQL_Driver.ReadColumn<double>(db.conn, table, "close", "1=1 ORDER BY Date ASC")).GetEnumerator())
                {
                    while (date.MoveNext() && rsi.MoveNext())
                    {
                        //Console.SetCursorPosition(0, Console.CursorTop - 1);

                        //Console.WriteLine($"{date.Current}      {rsi.Current}");
                        Dictionary<string, string> dic = new Dictionary<string, string>
                        {
                            { "RSI", rsi.Current.ToString() }
                        };
                        SQL_Driver.Update(db.conn, table, dic, $"Date = '{date.Current}'");
                    }
                }
            }
        }

        public static IEnumerable<Quote> GetQuotes(string table)
        {
            using (IEnumerator<string> date = SQL_Driver.ReadColumn<string>(db.conn, table, "Date", "1=1 ORDER BY Date ASC").GetEnumerator())
            {
                using (IEnumerator<NeaLibrary.DataStructures.Vector> data = SQL_Driver.ReadRow_AsVector(db.conn, table, new string[] { "open","high","low","close","volume" }, "1=1 ORDER BY Date ASC").GetEnumerator())
                {
                    while (date.MoveNext() && data.MoveNext())
                    {
                        Quote quote = new Quote();
                        quote.Date = DateTime.Parse(date.Current.ToString());
                        quote.Open = (decimal)data.Current[0];
                        quote.High = (decimal)data.Current[1];
                        quote.Low = (decimal)data.Current[2];
                        quote.Close = (decimal)data.Current[3];
                        quote.Volume = (decimal)data.Current[4];
                        yield return quote;
                    }
                }
            }
        }



        public static void CalculateStochastic(string table, int lookback = 14)
        {
            IEnumerable<StochResult> results = GetQuotes(table).GetStoch(lookback);
            foreach (StochResult r in results)
            {
                Dictionary<string,string> v = new Dictionary<string,string>();

                if (r.Oscillator == null)
                {
                    v.Add("StochasticOscillator", "0.5"); //default
                }
                else
                {
                    v.Add("StochasticOscillator", (r.Oscillator/100).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }
        public static void CalculateMacd(string table, int fast = 12, int slow=26)
        {
            IEnumerable<MacdResult> results = GetQuotes(table).GetMacd(fast, slow);
            foreach (MacdResult r in results)
            {
                Dictionary<string, string> v = new Dictionary<string, string>();

                if (r.Signal == null)
                {
                    v.Add("MACD", "0"); //default
                }
                else
                {
                    v.Add("MACD", (r.Signal).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }
        public static void CalculateSmi(string table, int lookback = 14, int firstsmoothing=3, int secondsmoothing=3)
        {
            IEnumerable<SmiResult> results = GetQuotes(table).GetSmi(lookback,firstsmoothing,secondsmoothing);
            foreach (SmiResult r in results)
            {
                Dictionary<string, string> v = new Dictionary<string, string>();

                if (r.Smi == null)
                {
                    v.Add("SMI", "0"); //default
                }
                else
                {
                    v.Add("SMI", (r.Signal).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }
        public static void CalculateCci(string table, int lookback = 20)
        {
            IEnumerable<CciResult> results = GetQuotes(table).GetCci(lookback);
            foreach (CciResult r in results)
            {
                Dictionary<string, string> v = new Dictionary<string, string>();

                if (r.Cci == null)
                {
                    v.Add("CCI", "0"); //default
                }
                else
                {
                    v.Add("CCI", (r.Cci/100).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }
        public static void CalculateSma(string table, int lookback = 14)
        {
            IEnumerator<Quote> qs = GetQuotes(table).GetEnumerator();
            IEnumerable<SmaResult> results = GetQuotes(table).GetSma(lookback);
            foreach (SmaResult r in results)
            {
                Dictionary<string, string> v = new Dictionary<string, string>();

                if (r.Sma == null)
                {
                    qs.MoveNext();
                    v.Add("SMA", qs.Current.Close.ToString()); //default
                }
                else
                {
                    v.Add("SMA", (r.Sma).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }

        public static void CalculateAdx(string table, int lookback = 14)
        {
            IEnumerable<AdxResult> results = GetQuotes(table).GetAdx(lookback);
            foreach (AdxResult r in results)
            {
                Dictionary<string, string> v = new Dictionary<string, string>();

                if (r.Adx == null)
                {
                    v.Add("ADX", "0"); //default
                }
                else
                {
                    v.Add("ADX", (r.Adx/100).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }

        public static void CalculateMfi(string table, int lookback = 14)
        {
            IEnumerable<MfiResult> results = GetQuotes(table).GetMfi(lookback);
            foreach (MfiResult r in results)
            {
                Dictionary<string, string> v = new Dictionary<string, string>();

                if (r.Mfi == null)
                {
                    v.Add("MFI", "0.5"); //default
                }
                else
                {
                    v.Add("MFI", (r.Mfi / 100).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }
        public static void CalculateAroon(string table, int lookback = 25)
        {
            IEnumerable<AroonResult> results = GetQuotes(table).GetAroon(lookback);
            foreach (AroonResult r in results)
            {
                Dictionary<string, string> v = new Dictionary<string, string>();

                if (r.Oscillator == null)
                {
                    v.Add("Aroon", "0"); //default
                }
                else
                {
                    v.Add("Aroon", (r.Oscillator/100).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }

        /// <summary>
        /// standard 20 day short term trading ema
        /// </summary>
        /// <param name="table"></param>
        /// <param name="lookback"></param>
        public static void CalculateEma(string table, int lookback = 20)
        {
            IEnumerator<Quote> qs = GetQuotes(table).GetEnumerator();
            IEnumerable<EmaResult> results = GetQuotes(table).GetEma(lookback);
            foreach (EmaResult r in results)
            {
                Dictionary<string, string> v = new Dictionary<string, string>();

                if (r.Ema == null)
                {
                    qs.MoveNext();
                    v.Add("EMA", qs.Current.Close.ToString()); //default
                }
                else
                {
                    v.Add("EMA", (r.Ema).ToString());
                }
                SQL_Driver.Update(db.conn, table, v, $"Date = '{r.Date.ToString("yyyy-MM-dd")}'");
            }
        }

        /// <summary>
        /// Look up all the methods to use in calculation of the specified table
        /// </summary>
        public static void Calculate(string table)
        {
            //string type = "";
            //using (var r = SQL_Driver.Query(db.conn, $"SELECT AssetType FROM TokenNames WHERE Token='{table}'"))
            //{
            //    type
            //}
            string json_list_commands = "";
            using (var r = SQL_Driver.Query(db.conn, $"SELECT CalculatorOperations FROM AssetType, TokenNames WHERE TokenNames.AssetType = AssetType.AssetType AND TokenNames.Token = '{table}';"))
            {
                r.Read();
                json_list_commands = r.GetString(0);
            }
            JArray jArray = JArray.Parse(json_list_commands);
            foreach (JValue jval in jArray)
            {
                string methodName = jval.ToString();
                string className = String.Join(".",methodName.Split(".").SkipLast(1));
                string funcName = methodName.Split(".").Last();
                var type = Type.GetType(className);
                
                try
                {
                    var methodInfo = type.GetMethod(funcName);
                    // according to https://stackoverflow.com/questions/11908156/call-static-method-with-reflection
                    //https://stackoverflow.com/questions/2421994/invoking-methods-with-optional-parameters-through-reflection
                    int paramcount= methodInfo.GetParameters().Length;
                    var param =new object[paramcount];
                    param[0] = table;
                    for(int i=1;i<methodInfo.GetParameters().Length;i++) param[i]=Type.Missing;
                    methodInfo.Invoke(null, param);
                }catch (Exception ex)
                {
                    continue;//multiple methods of same name, do nothing
                    //or invalid method invoked
                }
                
            }
        }

        //public static void CalculateRSI(string table, int n = 14)
        //{
        //    using (IEnumerator<string> date = SQL_Driver.ReadColumn<string>(db.conn, table, "Date", "1=1 ORDER BY Date ASC").GetEnumerator())
        //    {
        //        using (IEnumerator<double> rsi = Tools.Tools.RSI(SQL_Driver.ReadColumn<double>(db.conn, table, "close", "1=1 ORDER BY Date ASC")).GetEnumerator())
        //        {
        //            while (date.MoveNext() && rsi.MoveNext())
        //            {
        //                //Console.SetCursorPosition(0, Console.CursorTop - 1);

        //                //Console.WriteLine($"{date.Current}      {rsi.Current}");
        //                Dictionary<string, string> dic = new Dictionary<string, string>
        //                {
        //                    { "RSI", rsi.Current.ToString() }
        //                };
        //                SQL_Driver.Update(db.conn, table, dic, $"Date = '{date.Current}'");
        //            }
        //        }
        //    }
        //}
    }  
}

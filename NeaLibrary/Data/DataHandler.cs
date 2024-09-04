using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SQLite;

using NeaLibrary.DataStructures;
using System.Collections.Generic;
using System;
using System.IO;
using NeaLibrary.WebRequests;
using System.Configuration;
using NeaLibrary.Tools;
using Skender.Stock.Indicators;
using System.Globalization;

namespace NeaLibrary.Data
{
    public class Data_Handler{

        string? json_doc;

        SQL_Driver DB;
        
        public Data_Handler(string db){

            //string path = System.AppContext.BaseDirectory;
            //#nullable enable
            //path = Directory.GetParent(path).ToString();
            //path = Directory.GetParent(path).ToString();

            //path = Directory.GetParent(path).ToString();
            //path = Directory.GetParent(path).ToString();
            //#nullable disable
            //path+=$"/{db}";
            DB= new SQL_Driver(db);

         }
        public Data_Handler()
        {

            //            string path = System.AppContext.BaseDirectory;
            //#nullable enable
            //            path = Directory.GetParent(path).ToString();
            //            path = Directory.GetParent(path).ToString();

            //            path = Directory.GetParent(path).ToString();
            //            path = Directory.GetParent(path).ToString();
            //#nullable disable
            //            path += $"/{Tools.Tools.GetGlobalVar("db_dir")}";
            string path = Tools.Tools.GetGlobalVar("db_dir");
            DB = new SQL_Driver(path);

        }
        public (Vector[],Vector[]) produceTrainingData(){
            List<Vector> ins= new List<Vector>();

            using (SQLiteDataReader reader =SQL_Driver.Query(DB.conn,$"SELECT * FROM IBM WHERE Date>'2000-1-1'ORDER BY Date;")){
                reader.Read();

                double prev_open = (double)reader.GetValue(1);
                double prev_ema = (double)reader.GetValue(6);
                reader.Read();
                double open = (double)reader.GetValue(1);
                double ema = (double)reader.GetValue(6);
                Vector v = new Vector(2);
                v.vector = new double[]{open/prev_open,ema/prev_ema};
                ins.Add(v);
                prev_open = open; 
                prev_ema = ema;
                while(reader.Read()){
                    open = (double)reader.GetValue(1);
                    ema = (double)reader.GetValue(6);
                    Vector V = new Vector(2);
                    V.vector = new double[]{open/prev_open,ema/prev_ema};
                    prev_open = open; 
                    prev_ema = ema;
                    ins.Add(V);
                }
            }
            Vector[] INS = new Vector[ins.Count-1];
            Vector[] OUT = new Vector[ins.Count-1];
            for(int i =0;i<ins.Count-1;i++){
                INS[i] = ins[i];
                OUT[i] = ins[i+1];
            }
            
            return (INS,OUT);
            

         }

        /// <summary>
        /// Fetch data from API as described by Sources table in the database
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="args"></param>
        /// <param name="upsert">Determine whether Upsert behaviour is used, if false the strict Insert</param>
        public string Fetch(int SourceID,string[] args,bool upsert=true){
            int fetched = 0;
            string baseRequest;
            string requestArgs;
            long namePos;
            string assetType;
            string DDL_table_struct;
            string responsemap;
            string rootmap;
            string dateFormat;
            string culture;
            using (SQLiteDataReader reader =SQL_Driver.Query(DB.conn,$"SELECT Source, Parameters, AssetType, ResponseMap, ResponseRoot, NamePositionInParameters, DateFormat FROM Sources WHERE SourceID={SourceID}")){
                reader.Read();
                baseRequest = (string)reader.GetValue(0);
                requestArgs = (string)reader.GetValue(1);
                assetType = (string)reader.GetValue(2);
                responsemap = (string)reader.GetValue(3);
                rootmap = (string)reader.GetValue(4);
                namePos = (long)reader.GetValue(5);
                culture = (string)reader.GetValue(6);
                using (SQLiteDataReader reader2 =SQL_Driver.Query(DB.conn,$"SELECT Fields FROM AssetType WHERE AssetType='{assetType}'")){
                    reader2.Read();
                    DDL_table_struct = (string)reader2.GetValue(0);
                }
                using (SQLiteDataReader reader2 = SQL_Driver.Query(DB.conn, $"SELECT DateFormat FROM Culture WHERE Culture='{culture}'"))
                {
                    reader2.Read();
                    dateFormat = (string)reader2.GetValue(0);
                }
            }
            requestArgs=String.Format(requestArgs,args);
            string req = baseRequest+requestArgs;
            
            string json_doc = WebRequestsHandler.Req(req);
            
           JToken json = JToken.Parse(json_doc);

            Console.WriteLine($"Fetched {args[namePos]} from source {SourceID}: {req}");
            Console.WriteLine();
            JArray RootMap = (JArray)JsonConvert.DeserializeObject(rootmap);
            foreach(var d in RootMap){
                json = json[d.Value<string>()];
            }//json now reduced to root
            //i coded this before i found out JObject.SelectToken()  perhaps change in future
            

            SQL_Driver.CreateTable(DB.conn,args[namePos],DDL_table_struct);
            //if it doesnt exist for new tokens
            Dictionary<string,string> token = new Dictionary<string,string>();
            token.Add("Token", "'"+args[namePos]+"'");
            token.Add("AssetType", "'" + assetType+"'");
            SQL_Driver.InserOnConflictUpdate_Data(DB.conn, "TokenNames", "Token", "AssetType=excluded.AssetType",token);


            string table = args[namePos];

            // JObject values = (JObject)json;

            //var values = (json is JArray)? (JArray)json:(JObject)json;

            JObject ResponseMap = JObject.Parse(responsemap);
            
            void AddData(Dictionary<string,string> data)
            {
                if (!upsert)
                {
                    try
                    {
                        SQL_Driver.InsertData(DB.conn, table, data);
                        fetched++;
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.WriteLine($"Fetched results: {fetched}");
                    }
                    catch
                    {
                        //log

                    }
                }
                else
                {
                    SQL_Driver.Upsert(DB.conn, table, data, "Date");
                }
            }
            DateTime ParseDate(string date)
            {
                if (culture == "UnixMsec")
                {
                    return DateTime.UnixEpoch.AddMilliseconds(Convert.ToDouble(date));
                }
                else
                {
                    var dateculture = CultureInfo.CreateSpecificCulture(culture); //should be country code
                    return DateTime.ParseExact(date, dateFormat, dateculture);
                }
            }
            if (json is JArray) {
                var values = (JArray)json;
                string date_key = "";
                foreach (KeyValuePair<string, JToken> maptoken in ResponseMap)
                {
                    if ((string)maptoken.Value=="Date")
                    {
                        date_key = maptoken.Key;
                        break;
                    }
                }

                foreach (JObject quote in values) {

                    DateTime t = ParseDate((string)quote[date_key]);
                    Dictionary<string, string> dic = new Dictionary<string, string>();

                    dic.Add("Date", $"'{t.ToString("yyyy-MM-dd")}'");  //keep the quote marks?    want always the format yyyy-MM-dd
                                                                       //extra ' so text gets passed correctly
                                                                       //pair.Value[];
                    foreach (KeyValuePair<string, JToken> map in ResponseMap)
                    {
                        if (map.Key == date_key) continue;

                        //map.Key matches response. map.Value matches DB
                        string item_value = (string)quote[map.Key];
                        dic.Add($"{(string)map.Value}", item_value);
                    }
                    AddData(dic);
                }

            } else {

                var values = (JObject)json;
                foreach (KeyValuePair<string, JToken> quote in values)
                {
                    //foreach (JProperty record in cur.Properties())
                    //{
                    //Console.WriteLine($"{pair.Key}");
                    DateTime t = ParseDate(quote.Key);
                    //if (culture=="UnixMsec") {
                    //    t= DateTime.UnixEpoch.AddMilliseconds(Convert.ToDouble(quote.Key));
                    //}
                    //else
                    //{
                    //    t = DateTime.Parse(quote.Key);
                    //}
                    Dictionary<string, string> dic = new Dictionary<string, string>();

                    dic.Add("Date", $"'{t.ToString("yyyy-MM-dd")}'");  //keep the quote marks?    want always the format yyyy-MM-dd
                                                                       //extra ' so text gets passed correctly
                                                                       //pair.Value[];
                    foreach (KeyValuePair<string, JToken> map in ResponseMap)
                    {
                        //map.Key matches response. map.Value matches DB
                        string r = (string)quote.Value[map.Key];
                        dic.Add($"{(string)map.Value}", r);
                    }
                    AddData(dic);
                }
            }

            //Console.WriteLine("Complete!");
            return table;
        }

    }
}
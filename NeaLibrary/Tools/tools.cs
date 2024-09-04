using System;
using System.Text.Json;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using NeaLibrary.DataStructures;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Security.Cryptography;
using NeaLibrary.Data.Other;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace NeaLibrary.Tools
{
    public static partial class Tools{
        private static readonly Random rand;//=new Random();
        //private static readonly RandomNumberGenerator secure_rand;
        #pragma warning disable
        private static RNGCryptoServiceProvider secure_rand = new RNGCryptoServiceProvider();
        private static RandomHolder RandomHolder = new RandomHolder();

        private static object settinglock = new object();

        private static int seed = Environment.TickCount;
        public static event EventHandler SettingsChanged;
        private static string json_settings = File.ReadAllText("settings.json");   //should be included in the bin
        static Tools()
        {
            rand=new Random();
        }

        private static JsonSerializerOptions JSON_options = new JsonSerializerOptions{
            WriteIndented=true
            };

        /// <summary>
        /// Gets solution files directory, just usefyul for testing and coding
        /// </summary>
        /// <returns></returns>
        public static string get_root_folder_directory()
        {
            string path=System.AppContext.BaseDirectory;

            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();

            path = Directory.GetParent(path).ToString();
            path = Directory.GetParent(path).ToString();

            return path;
        }

        public static string GetGlobalVar(string s)
        {
            Newtonsoft.Json.Linq.JToken t = Newtonsoft.Json.Linq.JToken.Parse(json_settings);
            return t[s]["Value"].ToString().Trim();
        }
        public static string GetAllSettings()
        {
            return json_settings;
        }
        public static void SaveGlobalVar(string s, SettingsItem val)
        {
            lock (settinglock)
            {
                Newtonsoft.Json.Linq.JToken t = Newtonsoft.Json.Linq.JToken.Parse(json_settings);
                //problem before was that we took a class serialised, and replaced it with a string. so when it tries to deserialise it it fails
                //string serialisedSettingItem = JsonConvert.SerializeObject(val);
                JObject serialisedSettingItemJObject = JObject.FromObject(val);
                t[s] = serialisedSettingItemJObject;
                json_settings = t.ToString();
                File.WriteAllText($"{System.AppContext.BaseDirectory}/settings.json", json_settings);
                SettingsChanged(new Tuple<string, SettingsItem>(s, val), new EventArgs());
            }
        }
        public static void SetGlobalVar(string s, string val)
        {
            Newtonsoft.Json.Linq.JToken t = Newtonsoft.Json.Linq.JToken.Parse(json_settings);
            SettingsItem sett = t[s].ToObject<SettingsItem>();
            sett.Value = val;
            SaveGlobalVar(s, sett);
        }

        public static double RSI_Calc(double prev, double newaverage, double pricechange ,int n=14){
            double nposaverage;
            double nnegaverage;
            if(pricechange>0){
                nposaverage = (prev *(n-1)+pricechange)/n;
                nnegaverage = (prev *(n-1)+0)/n;
            }else{
                nposaverage = (prev *(n-1)+0)/n;
                nnegaverage = (prev *(n-1)+pricechange)/n;
            }
            double RS = nposaverage/nnegaverage;
            return 1/(1+RS);
        }

        public static double RandomDouble(double min, double max){
            return rand.NextDouble() * (max - min) + min;
        }
        public static double RandomDouble(){
            return rand.NextDouble();
        }
        public static int RandomInt(int min, int max){
            ///<summary>Inclusive random int</summary>
            return rand.Next(min,max+1);//+1 to make it inclusive
        }
        public static int SecureRandomInt(int min, int max)
        {
            ///<summary>Inclusive random int</summary>
            byte[] bytes = new byte[4];
            secure_rand.GetBytes(bytes);//+1 to make it inclusive
            return BitConverter.ToInt32(bytes, 0);
        }
        public static int RandomInt_ThreadSafe(int min, int max){
            int n;
            lock (rand)
            {
                n = rand.Next(min, max + 1);
            }
            return n;
        }


        public static double BiasedToStart(double min, double max){
            return Math.Floor(Math.Abs(RandomDouble() - RandomDouble()) * (1 + max - min) + min);
        }//https://gamedev.stackexchange.com/questions/116832/random-number-in-a-range-biased-toward-the-low-end-of-the-range
        //https://stackoverflow.com/questions/61056693/random-number-within-a-range-biased-towards-the-minimum-value-of-that-range
        //random biased functions credited to those sources
        public static double BiasedToStartSqrt(double min, double max){
            return (1 - Math.Sqrt(1 - RandomDouble()))*(max-min)+min;
        }
        public static int BiasedToStartInt(int min,int max){
            return (int)Math.Floor(BiasedToStartSqrt(min,max));
        }

        public static T[][] SplitArrayEvenly<T>(T[] v, int chunks){
            int chunksize = v.Length/chunks;
            T[][] r = new T[chunks][];
            for(int i =0; i<chunks-1; i++){
                 r[i] = new T[chunksize];
            }
            r[r.Length-1] = new T[v.Length%chunks];
            for(int i =0; i<chunks*chunksize; i++){
                int chunk=i/chunksize;
                r[chunk][i%chunksize]=v[i];
            }
            for(int i=chunks*chunksize;i<v.Length;i++){
                r[r.Length-1][i] = v[i];
            }

            return r;
        }
        public static int IndexOf<T>(IEnumerable<T> collection, T val)
        {
            int p = 0;
            foreach(T t in collection)
            {
                if (t.Equals(val)) return p;
                p++;
            }return -1;
        }
        public static void Serialize(string path,object obj){
            
            //File.WriteAllText(path,JsonSerializer.Serialize(obj,JSON_options));
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    #pragma warning disable
                    bf.Serialize(fs, obj);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw e;
            }
        }

        public static string CleanString(string s){
            string r="";
            foreach(char c in s){
                if(Char.IsLetter(c)){
                    r+=c;
                }else{
                    r+='_';
                }
            }return r;
        }
        public static string Join(IEnumerable<string> strings){
            return String.Join(",",strings);
        }

        public static void write_to_file(string message, string file)
        {
            using(StreamWriter sw = new StreamWriter(get_root_folder_directory()+"/"+file, true))
            {
                sw.WriteLine(message);
            }
        }
    }
}

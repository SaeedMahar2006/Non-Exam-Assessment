using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using NeaLibrary.Tools;
using NeaLibrary.DataStructures;
using System.Xml.Linq;
using Skender.Stock.Indicators;
using System.Net;

namespace NeaLibrary.Data
{
   public class SQL_Driver
   {
      public SQLiteConnection conn;
      
        public SQL_Driver(string source){
      
        conn = new SQLiteConnection($"Data Source={source};Version=3;", true);
         // Open the connection:
         try
         {
            conn.Open();
            //Console.WriteLine($"Opened db {source} connection at {conn}");
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
         }


        }


        /// <summary>
        /// Initiate a connection to the specified data base file.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
      public static SQLiteConnection CreateConnection(string source)
      {

         SQLiteConnection sqlite_conn;
         // Create a new database connection:
         sqlite_conn = new SQLiteConnection($"Data Source={source};");
         // Open the connection:

         try
         {
            sqlite_conn.Open();
         }
         catch (Exception ex)
         {

         }
         return sqlite_conn;
      }

      public static void CreateTable(SQLiteConnection conn,string name, Dictionary<string,string> columns)
      {

         SQLiteCommand sqlite_cmd;
         
         string col = "";
         foreach(KeyValuePair<string,string> entry in columns){
            col +=entry.Key+" "+ entry.Value +",";
         }
         string Createsql = $"CREATE TABLE [IF NOT EXISTS] {name}({col.Substring(0,col.Length-1)})";
         //string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
         
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = Createsql;
         sqlite_cmd.ExecuteNonQuery();
      }

        /// <summary>
        /// create a table. specify the DDL art of the CREATE TABLE command. by default uses if not exists
        /// specify brackets in the ddl
        /// 
        /// e.g.
        ///(
        ///colum1 INTEGER,
        ///column2 REAL,
        ///column3 TEXT
        ///)
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="name"></param>
        /// <param name="DDL"></param>
      public static void CreateTable(SQLiteConnection conn,string name, string DDL)
      {

         SQLiteCommand sqlite_cmd;
         string Createsql = $"CREATE TABLE IF NOT EXISTS \'{name}\' {DDL}";
         //string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
         
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = Createsql;
         sqlite_cmd.ExecuteNonQuery();
      }

      public static void DropTable(SQLiteConnection conn,string name)
      {

         SQLiteCommand sqlite_cmd;
         
         string Createsql = $"DROP TABLE '{name}';";
         //string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
         
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = Createsql;
         Console.WriteLine($"dropped {name}, affected {sqlite_cmd.ExecuteNonQuery()}");
      }
      public static void ClearTable(SQLiteConnection conn,string name)
      {

         SQLiteCommand sqlite_cmd;
         
         string Createsql = $"DELETE FROM {name};";
         //string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
         
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = Createsql;
         Console.WriteLine($"cleared {name}, affected {sqlite_cmd.ExecuteNonQuery()}");
      }

      public static void ExecCommand(SQLiteConnection conn, string com){
         SQLiteCommand command = conn.CreateCommand();
         command.CommandText = com;
         int n = command.ExecuteNonQuery();
         Console.WriteLine($"Executed: {com}\nAffected {n}");
      }
      public static SQLiteDataReader Query(SQLiteConnection conn, string q){

         SQLiteCommand cmd =  conn.CreateCommand();
         cmd.CommandText = q;
         SQLiteDataReader reader = cmd.ExecuteReader();

         return reader;
        }
        public static void InserOnConflictUpdate_Data(SQLiteConnection conn, string table, string conflict, string on_conflict,Dictionary<string, string> ins) {
            string col = Tools.Tools.Join(ins.Keys);
            string val = Tools.Tools.Join(ins.Values);
            string com = $"INSERT INTO {table}({col}) VALUES({val}) " +
                $"ON CONFLICT({conflict}) DO UPDATE SET {on_conflict};";
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = com;
            sqlite_cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// The key is which column, the value is the data
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="table"></param>
        /// <param name="ins"></param>
      public static void InsertData(SQLiteConnection conn,string table ,Dictionary<string,string> ins)
      {
         SQLiteCommand sqlite_cmd;
         sqlite_cmd = conn.CreateCommand();

         string col = "";
         string val="";
         //foreach(KeyValuePair<string,string> entry in ins){
            //col +=entry.Key+",";
            //val +=entry.Value+",";
         //}
         col= Tools.Tools.Join(ins.Keys);
         val= Tools.Tools.Join(ins.Values);

         //Console.WriteLine($"({col}) ({val})");
         //sqlite_cmd.CommandText = $"INSERT INTO {table}({col.Substring(0,col.Length-1)}) VALUES ({val.Substring(0,val.Length-1)});";
         sqlite_cmd.CommandText = $"INSERT INTO {table} ({col}) VALUES ({val});";

         sqlite_cmd.ExecuteNonQuery();
         

         /*
         sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES ('Test1 Text1 ', 2);";
         sqlite_cmd.ExecuteNonQuery();
         sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES ('Test2 Text2 ', 3);";
         sqlite_cmd.ExecuteNonQuery();
      

         sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES ('Test3 Text3 ', 3);";
         sqlite_cmd.ExecuteNonQuery();
         */
      }

      public static void Update(SQLiteConnection conn, string table, Dictionary<string,string> vals, string condition){
         string val="";
         foreach(KeyValuePair<string,string> r in vals){
            val += r.Key+"="+r.Value+",";
         }
         string sql = $"UPDATE \'{table}\' SET {val.Substring(0,val.Length-1)} WHERE {condition};";
         SQLiteCommand sqlite_cmd;
         sqlite_cmd = conn.CreateCommand();
         sqlite_cmd.CommandText = sql;
         sqlite_cmd.ExecuteNonQuery();
      }




        /// <summary>
        /// If record doesnt exist (based on PK) then Insert,
        /// Else Update
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="table"></param>
        /// <param name="ins"></param>
        public static void Upsert(SQLiteConnection conn, string table, Dictionary<string, string> vals, string conflict)
        {
            string update_string = "";
            foreach (KeyValuePair<string, string> r in vals)
            {
                update_string += r.Key + "=" + r.Value + ",";
            }
            string insert_col = Tools.Tools.Join(vals.Keys);
            string insert_val = Tools.Tools.Join(vals.Values);
            string sql = $"INSERT INTO {table} ({insert_col}) VALUES ({insert_val}) ON CONFLICT({conflict}) DO UPDATE SET {update_string.Substring(0,update_string.Length-1)};";
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = sql;
            sqlite_cmd.ExecuteNonQuery();
        }

      //  public static DataTable ReadData(SQLiteConnection conn,string query)
      //{
      //   DataTable table=new DataTable();
      //   SqliteDataAdapter adapter = new SqliteDataAdapter(query,conn);
      //   adapter.Fill(table);
      //   return table;
      //}

        public static IEnumerable<T> ReadColumn<T>(SQLiteConnection conn, string table, string col, string cond="1=1")
        {
            using (SQLiteDataReader r = Query(conn, $"SELECT {col} FROM {table} WHERE {cond};"))
            {
                while (r.Read())
                {
                    T value;
                    try
                    {
                        value = r.GetFieldValue<T>(0);
                    }
                    catch (Exception e)
                    {
                        yield break;
                    }
                    yield return value;
                }
            }
        }

        public static IEnumerable<Vector> ReadRow_AsVector(SQLiteConnection conn, string table, IEnumerable<string> cols, string cond = "1=1", bool DB_Null_Ignore=false, int DB_Null_Default_Index=-1)
        {

            using (SQLiteDataReader r = Query(conn, $"SELECT {string.Join(',',cols)} FROM {table} WHERE {cond};"))
            {
                while (r.Read())
                {
                    Vector v = new Vector(r.FieldCount);
                    int I=-1;
                    try
                    {
                        for (int i=0;i<r.FieldCount;i++)
                        {
                            I = i;
                            double d = r.GetFieldValue<double>(i);
                            v[i] = d;
                        }
                    }
                    catch (InvalidCastException e)
                    {
                        if(!DB_Null_Ignore) { yield break; }
                        v[I] = r.GetFieldValue<double>(DB_Null_Default_Index);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message); yield break;
                    }
                    yield return v;
                }
            }
        }

        /// <summary>
        /// SPECIFY COLUMNS IN THIS ORDER
        /// DATE
        /// OPEN
        /// HIGH
        /// LOW
        /// CLOSE
        /// VOLUME
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="table"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public static IEnumerable<Quote> ReadRow_AsQuote(SQLiteConnection conn, string table, IEnumerable<string> cols, int offset=0)
        {
            string c = String.Join(", ", cols);
            using (SQLiteDataReader r = Query(conn, $"SELECT {c} FROM {table} ORDER BY Date ASC LIMIT -1 OFFSET {offset};"))
            {
                while (r.Read())
                {
                    Quote q = new Quote();
                    try
                    {
                        q.Date = DateTime.Parse(r.GetFieldValue<string>(0));
                        q.Open = Convert.ToDecimal( r.GetFieldValue<double>(1) );
                        q.High = Convert.ToDecimal( r.GetFieldValue<double>(2) );
                        q.Low = Convert.ToDecimal( r.GetFieldValue<double>(3) );
                        q.Close = Convert.ToDecimal( r.GetFieldValue<double>(4) );
                        q.Volume = Convert.ToDecimal( r.GetFieldValue<double>(5) );
                    }
                    catch (InvalidCastException e)
                    {
                        continue;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message); yield break;
                    }
                    yield return q;
                }
            }
        }
    }
}
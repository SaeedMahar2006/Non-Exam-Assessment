using System;
//using System.Text.Json;
using System.Net;
using System.IO;
using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
namespace NeaLibrary.WebRequests
{
    public static class WebRequestsHandler
    {
        //private const string URL = @"http://www.alphavantage.co/query?function={}&symbol={}&outputsize=full&apikey={}";
        //private const string DATA = @"{""object"":{""name"":""Name""}}";

        public static string Req(string URL)
        {
#pragma warning disable
            WebRequest request = WebRequest.Create(URL);

            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                response.Close();
                return responseFromServer;
            }

        }
    }
}
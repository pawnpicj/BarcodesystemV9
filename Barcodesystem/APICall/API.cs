using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeLibrary.APICall
{
    public static class API
    {
        private static string server;
        private static HttpResponseMessage Response;
        //private static string BaseAddress = server;
        //private static string BaseAddress = "http://localhost:17079//api/";
        private static string errorMessage = "";

        public static bool IsConnected { get { return Read<bool>("util/isconnected"); } }
        public static DateTime ServerDatetime { get { return Read<DateTime>("util/getdate"); } }
        public static System.Net.HttpStatusCode StatusCode { get { return Response.StatusCode; } }
        public static string ErrorMessage { get { return errorMessage; } }
        public static string BaseAddress { get { return server; } set { server = value; } }
        public static string Url { get; set; }
        public static HttpClient Client
        {
            get
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Url);
                client.Timeout = new TimeSpan(0, 0, 500);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "dmFuc3lzdGVtQGdtYWlsLmNvbTpuc2NAdmFuc3lzdGVt");
                return client;
            }
            //set
            //{
            //    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value.ToString());
            //}
        }

        public static T Read<T>(string requestURI)
        {
            try
            {
                errorMessage = "";
                Response = Client.GetAsync(requestURI).Result;
                if (Response.IsSuccessStatusCode)
                {
                    if (Response.StatusCode == HttpStatusCode.OK)
                    {
                        //string aaa1 = JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);
                        return JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);
                    }
                    else if (Response.StatusCode == HttpStatusCode.NotFound)
                        throw new Exception("");
                    else if (Response.StatusCode == HttpStatusCode.BadRequest)
                        throw new Exception(Response.Content.ReadAsStringAsync().Result);
                    else throw new Exception("Invalid URI" + "\n\nReason Phrase: " + Response.ReasonPhrase);
                }
                else { throw new Exception("Invalid URI" + "\n\nReason Phrase: " + Response.ReasonPhrase); }
            }
            catch (Exception ex) { errorMessage = ex.Message; return (T)Convert.ChangeType(null, typeof(T)); }
        }

        public static HttpStatusCode Post(string requestURI, object value)
        {
            Response = Client.PostAsJsonAsync(requestURI, value).Result;
            return Response.StatusCode;
        }

        public static T PostWithReturn<T>(string requestURI, object value)
        {
            try
            {
                errorMessage = "";
                Response = Client.PostAsJsonAsync(requestURI, value).Result;
                //if (Response.IsSuccessStatusCode)
                //{
                    if (Response.StatusCode == HttpStatusCode.OK)
                        return JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);
                    else if (Response.StatusCode == HttpStatusCode.NotFound)
                        throw new Exception("");
                    if (Response.StatusCode == HttpStatusCode.BadRequest)
                        return JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);
                    throw new Exception("Invalid URI" + "\n\nReason Phrase: " + Response.ReasonPhrase);
                //}

                //throw new Exception("Post was not successful \n\nReason Phrase: " +
                //                    Response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);
                //return (T)Convert.ChangeType(null, typeof(T)); 
            }
        }

        public static List<T> PostWithReturnDataTableToList<T>(string requestURI, object sqlScript) where T : class, new()
        {
            try
            {
                DataTable table = PostWithReturn<DataTable>(requestURI, sqlScript);
                List<T> list = new List<T>();
                foreach (var row in table.AsEnumerable())
                {
                    var obj = row.ToObject<T>();
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
        public static T PostWithReturnDataTableToObject<T>(string requestURI, object sqlScript) where T : class, new()
        {
            try
            {
                DataTable table = PostWithReturn<DataTable>(requestURI, sqlScript);
                if (table.Rows.Count == 1)
                {
                    foreach (var row in table.AsEnumerable())
                    {
                        var obj = row.ToObject<T>();

                        return obj;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        private static T ToObject<T>(this DataRow row) where T : class, new()
        {
            T obj = new T();

            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.Name.Contains("Nullable"))
                    {
                        if (!string.IsNullOrEmpty(row[prop.Name].ToString()))
                            prop.SetValue(obj, Convert.ChangeType(row[prop.Name],
                            Nullable.GetUnderlyingType(prop.PropertyType), null));
                        //else do nothing
                    }
                    else
                        prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType), null);
                }
                catch
                {
                    continue;
                }
            }
            return obj;
        }
    }
}

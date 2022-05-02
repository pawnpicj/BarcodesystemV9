using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace BarCodeLibrary.APICall
{
    public static class API
    {
        private static HttpResponseMessage Response;

        //private static string BaseAddress = server;
        //private static string BaseAddress = "http://localhost:17079//api/";

        public static bool IsConnected => Read<bool>("util/isconnected");
        public static DateTime ServerDatetime => Read<DateTime>("util/getdate");
        public static HttpStatusCode StatusCode => Response.StatusCode;
        public static string ErrorMessage { get; private set; } = "";

        public static string BaseAddress { get; set; }

        public static string Url { get; set; }

        public static HttpClient Client
        {
            get
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(Url);
                client.Timeout = new TimeSpan(0, 0, 500);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", "dmFuc3lzdGVtQGdtYWlsLmNvbTpuc2NAdmFuc3lzdGVt");
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
                ErrorMessage = "";
                Response = Client.GetAsync(requestURI).Result;
                if (Response.IsSuccessStatusCode)
                {
                    if (Response.StatusCode ==
                        HttpStatusCode
                            .OK) //string aaa1 = JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);
                        return JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);
                    if (Response.StatusCode == HttpStatusCode.NotFound)
                        throw new Exception("");
                    if (Response.StatusCode == HttpStatusCode.BadRequest)
                        throw new Exception(Response.Content.ReadAsStringAsync().Result);
                    throw new Exception("Invalid URI" + "\n\nReason Phrase: " + Response.ReasonPhrase);
                }

                throw new Exception("Invalid URI" + "\n\nReason Phrase: " + Response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return (T)Convert.ChangeType(null, typeof(T));
            }
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
                ErrorMessage = "";
                Response = Client.PostAsJsonAsync(requestURI, value).Result;
                if (Response.IsSuccessStatusCode)
                {
                    if (Response.StatusCode == HttpStatusCode.OK)
                        return JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);
                    if (Response.StatusCode == HttpStatusCode.NotFound)
                        throw new Exception("");
                    if (Response.StatusCode == HttpStatusCode.BadRequest)
                        throw new Exception(Response.Content.ReadAsStringAsync().Result);
                    throw new Exception("Invalid URI" + "\n\nReason Phrase: " + Response.ReasonPhrase);
                }

                throw new Exception("Post was not successful \n\nReason Phrase: " +
                                    Response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return (T)Convert.ChangeType(null, typeof(T));
            }
        }

        public static List<T> PostWithReturnDataTableToList<T>(string requestURI, object sqlScript)
            where T : class, new()
        {
            try
            {
                var table = PostWithReturn<DataTable>(requestURI, sqlScript);
                var list = new List<T>();
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
                var table = PostWithReturn<DataTable>(requestURI, sqlScript);
                if (table.Rows.Count == 1)
                    foreach (var row in table.AsEnumerable())
                    {
                        var obj = row.ToObject<T>();

                        return obj;
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
            var obj = new T();

            foreach (var prop in obj.GetType().GetProperties())
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
                    {
                        prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType), null);
                    }
                }
                catch
                {
                }

            return obj;
        }
    }
}
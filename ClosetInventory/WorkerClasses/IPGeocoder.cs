using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ClosetInventory.WorkerClasses

{
    public class IPRootObject
    {
        public string status { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string timezone { get; set; }
        public string isp { get; set; }
        public string org { get; set; }
        public string @as { get; set; }
        public string query { get; set; }
    }
    public class IPGeocoder
    {
        string IP = String.Empty;
        string city = "";
        public async Task<string> GetCityNameOfIP()
        {
            if (city == "")
            {
                var rootObject = await GetLocationOfIP();
                city = rootObject.city;
                return city;
            }
            else
            {
                return city;
            }
        }

        public async Task<IPRootObject> GetLocationOfIP()
        {
            string ipAddress = GetUserIP(); //"12.247.118.54";
            string apiUrl = "http://ip-api.com/json";

            using (var client = new HttpClient())
            {
                string repUrl = apiUrl + "/" + ipAddress;
                HttpResponseMessage response = await client.GetAsync(repUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var rootResult = JsonConvert.DeserializeObject<IPRootObject>(result);
                    return rootResult;
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetUserIP()
        {
            
            HttpRequest http = HttpContext.Current.Request;

           
            if (http.ServerVariables["HTTP_CLIENT_IP"] != null)
            {
                IP = http.ServerVariables["HTTP_CLIENT_IP"].ToString();
            }
            else if (http.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                IP = http.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
           
            //else if
            //((http.UserHostAddress.Length != 0) && ((http.UserHostAddress != "::1") || (http.UserHostAddress != "localhost")))
            //{
            //    IP = http.UserHostAddress;
            //}
            else
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    IP = reader.ReadToEnd();
                }
                int i1 = IP.IndexOf("Address: ") + 9;
                int i2 = IP.LastIndexOf("</body>");
                IP = IP.Substring(i1, i2 - i1);
            }
            return IP;
        }
    }
}
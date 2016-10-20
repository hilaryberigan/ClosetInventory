using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ClosetInventory.WorkerClasses
{
    public class WeatherSearch
    {
        
        public static async Task<List<List>> GetRootObject()
        {
            
            IPGeocoder geocoder = new IPGeocoder();
            string apiUrl = "http://api.openweathermap.org/data/2.5/forecast?q=";
            string city = await geocoder.GetCityNameOfIP();
            string apiKey = "&APPID=3359bd7d84fe595e20f8e8e5b74c9d47";

            using (var client = new HttpClient())
            {
                string repUrl = apiUrl + city + "/" + apiKey;

                HttpResponseMessage response = await client.GetAsync(repUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var rootResult = JsonConvert.DeserializeObject<WeatherInformation>(result);
                    return rootResult.list;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
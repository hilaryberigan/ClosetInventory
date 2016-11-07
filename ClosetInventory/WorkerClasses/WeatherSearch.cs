using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using ClosetInventory.Models;

namespace ClosetInventory.WorkerClasses
{
    public class WeatherSearch
    {
        double temperature = 70;
    
    
        public async Task<double> GetTemperature(Weather weatherModel)
        {
    
            if (weatherModel.Date != DateTime.Today)
            { 
            IPGeocoder geocoder = new IPGeocoder();
            string apiUrl = "http://api.openweathermap.org/data/2.5/forecast?q=";
            string city = await geocoder.GetCityNameOfIP();
            string apiKey = "&APPID=3359bd7d84fe595e20f8e8e5b74c9d47";

            using (var client = new HttpClient())
            {
                string repUrl = apiUrl + city + "/" + apiKey;
                List<WeatherList> weatherList = null;
                HttpResponseMessage response = await client.GetAsync(repUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var rootResult = JsonConvert.DeserializeObject<WeatherInformation>(result);
                     weatherList = rootResult.list;

                    
                }
        


             

                double[] todayTemps =
                {
                     ((weatherList[0].main.temp - 273.15) * 9/5) + 32,
                     ((weatherList[1].main.temp - 273.15) * 9/5) + 32,
                     ((weatherList[2].main.temp - 273.15) * 9/5) + 32,
                     ((weatherList[3].main.temp - 273.15) * 9/5) + 32,
                     ((weatherList[4].main.temp - 273.15) * 9/5) + 32,
                     ((weatherList[5].main.temp - 273.15) * 9/5) + 32,
                     ((weatherList[6].main.temp - 273.15) * 9/5) + 32,
                     ((weatherList[7].main.temp - 273.15) * 9/5) + 32


                };
               
                temperature = todayTemps.Max();
                weatherModel.Temperature = temperature;
                }
                
            }
            return weatherModel.Temperature;
        }
    }
}
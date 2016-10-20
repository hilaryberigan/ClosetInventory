using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;

namespace ClosetInventory.WorkerClasses
{
    public class TrendGenerator
    {
        public async void GenerateTrendCombinations()
        {

            ArrayList results = await GetSearchResults();
            foreach (KeyValuePair<string, ArrayList> item in results)
            {

            }

        }

        static async Task<ArrayList> GetSearchResults()
        {

            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

    
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ea75f488d5ba4b1db07bc10c64ae7a3e");

       
            queryString["mkt"] = "en-US";
            queryString["filterResponse"] = "images";
            queryString["freshness"] = "month";

            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/images/search?q=chictopia.com&" + queryString;

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                //probably need try catch

                Dictionary<string, object> rawResults = serializer.Deserialize<Dictionary<string, object>>(result);

                string key = "value";

                List<KeyValuePair<string, object>> resultsInMotion = new List<KeyValuePair<string, object>>();

                ArrayList searchResults = new ArrayList();

                foreach (var item in rawResults)
                {
                    resultsInMotion.Add(item);
                }

                for (int i = 1; i < resultsInMotion.Count; i++)
                {
                    if (resultsInMotion[i].Key == key)
                    {
                        searchResults = (ArrayList)resultsInMotion[i].Value;
                    }

                }
                return searchResults;
            }

            else return null;
        }
    }

}

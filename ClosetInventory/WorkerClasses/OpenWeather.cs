using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ClosetInventory.WorkerClasses
{
    //public class Coord
    //{
    //    public double lon { get; set; }
    //    public double lat { get; set; }
    //}

    //public class Sys
    //{
    //    public string country { get; set; }
    //    public double sunrise { get; set; }
    //    public double sunset { get; set; }
    //}

    //public class APIWeather
    //{
    //    public int id { get; set; }
    //    public string main { get; set; }
    //    public string description { get; set; }
    //    public string icon { get; set; }
    //}

    //public class Main
    //{
    //    public double temp { get; set; }
    //    public double humidity { get; set; }
    //    public double pressure { get; set; }
    //    public double temp_min { get; set; }
    //    public double temp_max { get; set; }
    //}

    //public class Wind
    //{
    //    public double speed { get; set; }
    //    public double deg { get; set; }
    //}

    //public class Rain
    //{
    //    public double __invalid_name__3h { get; set; }
    //}

    //public class Clouds
    //{
    //    public double all { get; set; }
    //}

    //public class RootObject
    //{
    //    public Coord Coord { get; set; }
    //    public Sys Sys { get; set; }
    //    public List<APIWeather> APIweather { get; set; }
    //    public Main Main { get; set; }
    //    public Wind Wind { get; set; }
    //    public Rain Rain { get; set; }
    //    public Clouds Clouds { get; set; }
    //    public double dt { get; set; }
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public double cod { get; set; }
    //}

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Sys
    {
        public int population { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public Sys sys { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double sea_level { get; set; }
        public double grnd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }
    }

    public class APIWeather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Rain
    {
        public double? __invalid_name__3h { get; set; }
    }

    public class Sys2
    {
        public string pod { get; set; }
    }

    public class WeatherList
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public List<APIWeather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public Sys2 sys { get; set; }
        public string dt_txt { get; set; }
    }

    public class WeatherInformation
    {
        public City city { get; set; }
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<WeatherList> list { get; set; }
    }
}

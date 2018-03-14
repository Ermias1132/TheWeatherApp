using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheWeatherApp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheWeatherApp.Models;

namespace TheWeatherApp.Services
{
    public class WeatherService
    {
        public async Task<List<WeatherLocationInfo>> GetWeatherInfo(List<string> zipCodes)
        {
            List<WeatherLocationInfo> weatherInfoList = new List<WeatherLocationInfo>();
            foreach (var zipCode in zipCodes)
            {
                var url = WebUtility.UrlDecode($"https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"{zipCode}, US\")&format=json");
                var obj = await GetObject<RootObject>(url);

                weatherInfoList.Add(new WeatherLocationInfo {Latitude = obj.query.results.channel.item.lat.ToDouble(), Longitude = obj.query.results.channel.item.@long.ToDouble(), ZipCode = zipCode, FormattedLocation = obj.query.results.channel.location.city + ", " + obj.query.results.channel.location.region, Temperature = obj.query.results.channel.item.condition.temp });
            }

            return weatherInfoList;
        }

        public async Task<WeatherLocationInfo> GetWeatherInfo(double latitude, double longitude)
        {
            var url = WebUtility.UrlDecode($"https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text=\"{latitude}, {longitude}\")&format=json");
            var obj = await GetObject<RootObject>(url);

            return new WeatherLocationInfo { Latitude = obj.query.results.channel.item.lat.ToDouble(), Longitude = obj.query.results.channel.item.@long.ToDouble(), /*ZipCode = zipCode,*/ FormattedLocation = obj.query.results.channel.location.city + ", " + obj.query.results.channel.location.region, Temperature = obj.query.results.channel.item.condition.temp };

            //return await Task.Factory.StartNew(() => new WeatherLocationInfo
            //    {
            //        Temperature = "50",
            //        ZipCode = "30017", //Snellville
            //        Latitude = 33.857328,
            //        Longitude = -84.019911
            //    }
            //);
        }

        private async Task<T> GetObject<T>(string url)
        {
            var rxcui = "198440";
            var request = WebRequest.Create(string.Format(url, rxcui));
            request.ContentType = "application/json";
            request.Method = "GET";

            var response = await request.GetResponseAsync();

            //if (response.Status != TaskStatus.RanToCompletion)
            //    return default(T);

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                var content = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(content))
                {
                    return default(T);
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<T>(content);
                    return obj;
                }
            }
        }
    }
}

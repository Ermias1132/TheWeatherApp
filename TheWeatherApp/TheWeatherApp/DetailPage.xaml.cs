using System.Collections.Generic;
using System.Linq;
using TheWeatherApp.Helpers;
using TheWeatherApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TheWeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage()
        {
            InitializeComponent();

        }

        public DetailPage(List<WeatherLocationInfo> weatherInfoList) : this()
        {
            var latitudes = new List<double>();
            var longitudes = new List<double>();

            foreach (WeatherLocationInfo weatherInfo in weatherInfoList)
            {
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(weatherInfo.Latitude, weatherInfo.Longitude),
                    Label = weatherInfo.FormattedLocation,
                    Address =  "Temperature: " + weatherInfo.Temperature
                };

                weatherMap.Pins.Add(pin);

                latitudes.Add(weatherInfo.Latitude);
                longitudes.Add(weatherInfo.Longitude);
            }

            double lowestLat = latitudes.Min();
            double highestLat = latitudes.Max();
            double lowestLong = longitudes.Min();
            double highestLong = longitudes.Max();
            double centerLat = (lowestLat + highestLat) / 2;
            double centerLong = (lowestLong + highestLong) / 2;
            double distance = Utilities.CalculateGeoDistance(lowestLat, lowestLong, highestLat, highestLong);

            this.weatherMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(centerLat, centerLong), Distance.FromMiles(distance)));
        }
    }
}
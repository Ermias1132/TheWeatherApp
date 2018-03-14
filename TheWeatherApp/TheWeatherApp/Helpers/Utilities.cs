using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using TheWeatherApp.Models;
using Position = Xamarin.Forms.Maps.Position;

namespace TheWeatherApp.Helpers
{
    public static class Utilities
    {
        private const double EarthRadiusInMiles = 3956.0;

        private static double ToRadian(double val) { return val * (Math.PI / 180); }
        private static double RadianDiff(double val1, double val2) { return ToRadian(val2) - ToRadian(val1); }

        public static double CalculateGeoDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radius = EarthRadiusInMiles;

            return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((RadianDiff(lat1, lat2)) / 2.0), 2.0) + Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) * Math.Pow(Math.Sin((RadianDiff(lng1, lng2)) / 2.0), 2.0)))));
        }

        public static async Task<UserPosition> GetUserLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                var cancelSource = new CancellationTokenSource();
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

                if (locator.IsGeolocationEnabled)
                {
                    return new UserPosition {Latitude = position.Latitude, Longitude= position.Longitude, Success = true};
                }

                return new UserPosition { ErrorMessage = Enums.LocationErrorMessage.Unauthorized };
            }
            catch (GeolocationException geoEx)
            {
                if (geoEx.Error == GeolocationError.PositionUnavailable)
                {
                    return new UserPosition {ErrorMessage= Enums.LocationErrorMessage.PositionUnavailable} ;
                }
                else if (geoEx.Error == GeolocationError.Unauthorized)
                {
                    return new UserPosition { ErrorMessage = Enums.LocationErrorMessage.Unauthorized };
                }
                else
                {
                    return new UserPosition { ErrorMessage = Enums.LocationErrorMessage.UnknownNetworkError };
                }
            }
            catch (Exception e)
            {
                return new UserPosition { ErrorMessage = Enums.LocationErrorMessage.UnknownNetworkError };
            }
        }
    }
}

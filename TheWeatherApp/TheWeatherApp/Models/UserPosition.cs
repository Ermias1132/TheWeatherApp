using TheWeatherApp.Helpers;

namespace TheWeatherApp.Models
{
    public class UserPosition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Success { get; set; }
        public Enums.LocationErrorMessage ErrorMessage { get; set; }
    }
}

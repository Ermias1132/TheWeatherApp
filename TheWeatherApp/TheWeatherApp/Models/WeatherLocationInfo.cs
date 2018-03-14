namespace TheWeatherApp.Models
{
    public class WeatherLocationInfo
    {
        public string Temperature { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FormattedLocation { get; set; }
    }
}
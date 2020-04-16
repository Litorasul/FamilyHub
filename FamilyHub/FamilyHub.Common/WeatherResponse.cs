namespace FamilyHub.Common
{
    public class WeatherResponse
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string MainWeather { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public string IconSrc => $"http://openweathermap.org/img/wn/{this.Icon}@2x.png";

        public double TemperatureCurrent { get; set; }

        public double TemperatureMinimum { get; set; }

        public double TemperatureMaximum { get; set; }
    }
}

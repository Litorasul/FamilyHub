namespace FamilyHub.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Common;
    using Microsoft.Extensions.Options;
    using OpenWeatherAPI;

    public class WeatherService : IWeatherService
    {
        private readonly IOptions<OpenWeatherSettings> weatherConfig;

        public WeatherService(IOptions<OpenWeatherSettings> weatherConfig)
        {
            this.weatherConfig = weatherConfig;
        }

        public WeatherResponse GetCurrentWeather(double lat, double lon)
        {
            var queryString = $"lat={lat}&lon={lon}";
            var weatherApi = new API(this.weatherConfig.Value.ApiKey);
            var query = weatherApi.Query(queryString);
            var result = new WeatherResponse
            {
                Name = query.Name,
                MainWeather = query.Weathers.FirstOrDefault().Main,
                Description = query.Weathers.FirstOrDefault().Description,
                Icon = query.Weathers.FirstOrDefault().Icon,
                TemperatureCurrent = query.Main.Temperature.CelsiusCurrent,
                TemperatureMinimum = query.Main.Temperature.CelsiusMinimum,
                TemperatureMaximum = query.Main.Temperature.CelsiusMaximum,
            };
            return result;
        }
    }
}

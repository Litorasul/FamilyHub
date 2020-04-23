namespace FamilyHub.Services.Data.Weather
{
    using FamilyHub.Common;

    public interface IWeatherService
    {
        WeatherResponse GetCurrentWeather(double lat, double lon);
    }
}

namespace FamilyHub.Services.Data
{
    using System.Threading.Tasks;

    using FamilyHub.Common;

    public interface IWeatherService
    {
        WeatherResponse GetCurrentWeather(double lat, double lon);
    }
}

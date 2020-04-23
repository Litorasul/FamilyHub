namespace FamilyHub.Services.Data.Tests.Weather
{
    using FamilyHub.Common;
    using Microsoft.Extensions.Options;
    using Moq;

    public class WeatherServiceTests
    {
        private readonly IOptions<OpenWeatherSettings> weatherConfig;

        public WeatherServiceTests()
        {
            this.weatherConfig = new Mock<IOptions<OpenWeatherSettings>>().Object;
        }
    }
}

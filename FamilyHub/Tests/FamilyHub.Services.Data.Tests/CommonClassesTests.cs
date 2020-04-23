namespace FamilyHub.Services.Data.Tests
{
    using FamilyHub.Common;
    using Xunit;

    public class CommonClassesTests
    {
        [Theory]
        [InlineData("FamilyHub", GlobalConstants.SystemName)]
        [InlineData("Administrator", GlobalConstants.AdministratorRoleName)]
        public void GlobalConstantsTest(string expected, string actual)
        {
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeatherResponseShouldCreateCorrectIconSrc()
        {
            var response = new WeatherResponse
            {
                Country = "NO",
                Description = "aaa",
                Icon = "test",
                MainWeather = "bbb",
                Name = "ccc",
                TemperatureCurrent = 22.2,
                TemperatureMaximum = 24,
                TemperatureMinimum = 19,
            };

            Assert.Equal("http://openweathermap.org/img/wn/test@2x.png", response.IconSrc);
        }
    }
}

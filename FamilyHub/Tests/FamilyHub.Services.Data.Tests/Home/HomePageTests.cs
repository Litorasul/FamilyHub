namespace FamilyHub.Services.Data.Tests.Home
{
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using FamilyHub.Web;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class HomePageTests
    {
        [Theory]
        [InlineData("Photos/AllAlbums")]
        [InlineData("Photos/CreateAlbum")]
        [InlineData("Events/Create")]
        [InlineData("Events/All")]
        [InlineData("Events/Calendar")]
        [InlineData("Lists/AllToDo")]
        [InlineData("Lists/AllShopping")]
        [InlineData("Lists/AllChores")]
        [InlineData("Lists/Create")]
        public async Task LayoutShouldContainLinks(string expected)
        {
            var serverFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(
                b => b.ConfigureTestServices(s =>
                {
                    s.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                            "Test", options => { });
                }));
            var client = serverFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            var response = await client.GetAsync("/");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }

        [Theory]
        [InlineData(@"span id=""currentTemp""")]
        [InlineData(@"span id=""icon""")]
        [InlineData(@"span id=""name""")]
        [InlineData(@"span id=""country""")]
        [InlineData(@"span id=""maxTemp""")]
        [InlineData(@"span id=""minTemp""")]
        public async Task IndexShouldContainWeatherElements(string expected)
        {
            var serverFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(
                b => b.ConfigureTestServices(s =>
                {
                    s.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                            "Test", options => { });
                }));
            var client = serverFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            var response = await client.GetAsync("/");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }

        [Theory]
        [InlineData(@"name=""PostId""")]
        [InlineData(@"name=""Text""")]
        [InlineData(@"name=""AddComment""")]
        [InlineData(@"name=""Content""")]
        public async Task IndexShouldContainWallPostElements(string expected)
        {
            var serverFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(
                b => b.ConfigureTestServices(s =>
                {
                    s.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                            "Test", options => { });
                }));
            var client = serverFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            var response = await client.GetAsync("/");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }
    }
}

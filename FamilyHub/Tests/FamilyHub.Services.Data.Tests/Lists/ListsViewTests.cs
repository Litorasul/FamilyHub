namespace FamilyHub.Services.Data.Tests.Lists
{
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using FamilyHub.Web;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ListsViewTests
    {
        [Theory]
        [InlineData(@"<h1 class=""display-4""")] // Date.
        [InlineData(@"<div class=""card-deck")]
        [InlineData(@"<i class=""ion ion-settings mr-1""></i>")] // At least one element on the page
        public async Task AllChoresViewTests(string expected)
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

            var response = await client.GetAsync("Lists/AllChores");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }

        [Theory]
        [InlineData(@"<h1 class=""display-4""")] // Date.
        [InlineData(@"<div class=""card-deck")]
        [InlineData(@"<i class=""ion ion-ios-cart-outline mr-1""></i>")] // At least one element on the page
        public async Task AllShoppingViewTests(string expected)
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

            var response = await client.GetAsync("Lists/AllShopping");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }

        [Theory]
        [InlineData(@"<h1 class=""display-4""")] // Date.
        [InlineData(@"<div class=""card-deck")]
        [InlineData(@"<i class=""ion ion-clipboard mr-1""></i>")] // At least one element on the page
        public async Task AllToDoViewTests(string expected)
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

            var response = await client.GetAsync("Lists/AllToDo");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }

        [Theory]
        [InlineData(@"<form role=""form"" method=""post""")] // Date.
        [InlineData(@"id=""Title""")]
        [InlineData(@"id=""Description""")]
        [InlineData(@"id=""Type""")]
        [InlineData(@"<input type=""submit""")]
        [InlineData(" $.ajax({")]
        [InlineData("url: '/Lists/AddListItem'")]
        public async Task CreateGetViewTests(string expected)
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

            var response = await client.GetAsync("Lists/Create");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }
    }
}

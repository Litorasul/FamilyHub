namespace FamilyHub.Services.Data.Tests.Planner
{
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using FamilyHub.Web;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class EventViewsTests
    {
        [Theory]
        [InlineData("Create new event!</a>")] // Create New Event Button.
        [InlineData("<p>Created by:")] // At least one Event on the page.
        public async Task AllViewTests(string expected)
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

            var response = await client.GetAsync("Events/All");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }

        [Theory]
        [InlineData(@"<div id=""calendar""></div>")] // The Actual Calendar
        [InlineData("var calendar = new FullCalendar.Calendar")] // JQuery Calendar
        [InlineData("url: '/Events/GetEvents'")] // Get All Events Request
        [InlineData("calendar.render();")]
        public async Task CalendarViewTests(string expected)
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

            var response = await client.GetAsync("Events/Calendar");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }

        [Theory]
        [InlineData(@" <form role=""form"" method=""post""")]
        [InlineData(@"<input type=""submit""")]
        [InlineData(@"id=""Title""")]
        [InlineData(@"id=""Description""")]
        [InlineData(@"id=""End""")]
        [InlineData(@"id=""Color""")]
        [InlineData(@"id=""AssignedUsersId""")]
        [InlineData(@"id=""IsAllDay""")]
        [InlineData("$('.select2').select2();")]
        [InlineData("$('.my-colorpicker2').colorpicker();")]
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

            var response = await client.GetAsync("Events/Create");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }
    }
}

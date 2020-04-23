namespace FamilyHub.Services.Data.Tests.Photos
{
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using FamilyHub.Web;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class PhotosViewsTests
    {
        [Theory]
        [InlineData(@"<div class=""card-deck"">")]
        [InlineData("<p>Created by:")] // At least one Event on the page.
        public async Task AllAlbumsViewTests(string expected)
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

            var response = await client.GetAsync("Photos/AllAlbums");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }

        [Theory]
        [InlineData(@"<form role=""form"" method=""post""")]
        [InlineData(@"<input type=""submit""")]
        [InlineData(@"id=""Title""")]
        [InlineData(@"id=""Description""")]
        [InlineData(@"id=""Picture""")]
        [InlineData(@"enctype=""multipart/form-data""")]
        public async Task CreateAlbumGetViewTests(string expected)
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

            var response = await client.GetAsync("Photos/CreateAlbum");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }
    }
}

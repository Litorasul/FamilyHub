namespace FamilyHub.Services.Data.Tests.Messenger
{
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using FamilyHub.Web;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class MessengerViewTests
    {
        [Theory]
        [InlineData(@"<div class=""direct-chat-messages"">")]
        [InlineData(@"<div class=""direct-chat-text"">")]
        [InlineData(@"id=""sendButton""")]
        [InlineData("new signalR.HubConnectionBuilder()")]
        [InlineData(@"connection.invoke(""Send"", message);")]
        public async Task ChatViewTests(string expected)
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

            var response = await client.GetAsync("Messenger/Chat");

            var responseAsString = await response.Content.ReadAsStringAsync();

            Assert.Contains(expected, responseAsString);
        }
    }
}

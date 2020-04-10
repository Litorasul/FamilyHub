using System.Threading.Tasks;

namespace FamilyHub.Web.Hubs
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class MessengerHub : Hub 
    {
        public async Task Send(string message)
        {

        }
    }
}
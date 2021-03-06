﻿namespace FamilyHub.Web.Hubs
{
    using System.Threading.Tasks;

    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
    using FamilyHub.Services.Data.Messenger;
    using FamilyHub.Web.ViewModels.Messenger;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class MessengerHub : Hub
    {
        private readonly IMessengerService messengerService;
        private readonly UserManager<ApplicationUser> userManager;

        public MessengerHub(IMessengerService messengerService, UserManager<ApplicationUser> userManager)
        {
            this.messengerService = messengerService;
            this.userManager = userManager;
        }

        public async Task Send(string text)
        {
            var sanitizedText = new HtmlSanitizer().Sanitize(text);
            var userId = this.userManager.GetUserId(this.Context.User);
            var message = await this.messengerService.AddMessage<MessageInHubViewModel>(userId, sanitizedText);
            await this.Clients.All.SendAsync(
                "NewMessage",
                message);
        }
    }
}

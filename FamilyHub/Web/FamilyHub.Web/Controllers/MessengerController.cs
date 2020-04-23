namespace FamilyHub.Web.Controllers
{
    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
    using FamilyHub.Services.Data.Messenger;
    using FamilyHub.Web.ViewModels.Messenger;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MessengerController : Controller
    {
        private readonly IMessengerService messengerService;
        private readonly UserManager<ApplicationUser> userManager;

        public MessengerController(IMessengerService messengerService, UserManager<ApplicationUser> userManager)
        {
            this.messengerService = messengerService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Chat()
        {
            var viewModel = new MessagesPerConversationViewModel()
            {
                Messages = this.messengerService.GetAllMessages<MessageViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
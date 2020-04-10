namespace FamilyHub.Web.Controllers
{
    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
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
        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = new ConversationAllViewModel()
            {
                Conversations = this.messengerService.GetAllConversation<ConversationViewModel>(userId),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return this.View();
        }
    }
}
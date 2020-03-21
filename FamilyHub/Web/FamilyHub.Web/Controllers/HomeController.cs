using System.Threading.Tasks;
using FamilyHub.Web.ViewModels.Notifications;

namespace FamilyHub.Web.Controllers
{
    using System.Diagnostics;

    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly INotificationsService notificationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(INotificationsService notificationsService, UserManager<ApplicationUser> userManager)
        {
            this.notificationsService = notificationsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new NotificationAllViewModel
            {
                Notifications = this.notificationsService.GetAllByUser<NotificationSingleViewModel>(user.Id),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

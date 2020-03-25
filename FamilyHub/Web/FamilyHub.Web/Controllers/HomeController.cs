using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyHub.Data.Models.Planner;
using FamilyHub.Web.ViewModels.Events;
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
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            INotificationsService notificationsService, 
            IEventsService eventsService,
            UserManager<ApplicationUser> userManager)
        {
            this.notificationsService = notificationsService;
            this.eventsService = eventsService;
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
            foreach (var notification in viewModel.Notifications)
            {
                var currentEvent =
                    this.eventsService.GetById<EventNotificationViewModel>(notification.NotificationTypeId);
                notification.NotificationTypeTitle = $"{currentEvent.StartTime:dddd, dd MMMM yyyy} - {currentEvent.Title}";
                notification.NotificationTypeDescription = currentEvent.Description;
            }

            return this.View(viewModel);
        }

        [Authorize]
        public JsonResult GetEvents()
        {
            var events = this.eventsService.GetAll<EventCalendarViewModel>();

            return new JsonResult(events);
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

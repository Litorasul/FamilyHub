namespace FamilyHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FamilyHub.Data.Models;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Data;
    using FamilyHub.Services.Mapping;
    using FamilyHub.Web.ViewModels.Events;
    using FamilyHub.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public EventsController(IEventService eventService, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this.eventService = eventService;
            this.userManager = userManager;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult ByName(string name)
        {
            var viewModel = this.eventService.GetByName<EventViewModel>(name);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var users = this.userService.GetAll<UserDropDownViewModel>();
            var viewModel = new EventCreateInputModel
            {
                Users = users,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(EventCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var eventId = await this.eventService.CreateAsync(
                input.Title,
                input.Description,
                input.StartTime,
                input.Duration,
                input.IsFullDayEvent,
                input.IsRecurring,
                user.Id,
                input.AssignedUsersId);

            return this.Redirect("/");
        }
    }
}

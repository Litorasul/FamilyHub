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
        private readonly IEventsService eventsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public EventsController(IEventsService eventsService, UserManager<ApplicationUser> userManager, IUsersService usersService)
        {
            this.eventsService = eventsService;
            this.userManager = userManager;
            this.usersService = usersService;
        }

        [Authorize]
        public IActionResult All()
        {
            var viewModel = new EventAllViewModel()
            {
                Events =
                    this.eventsService.GetAll<EventSingleViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Calendar()
        {
            return this.View();
        }

        [Authorize]
        public JsonResult GetEvents()
        {
            var events = this.eventsService.GetAll<EventCalendarViewModel>();

            return new JsonResult(events);
        }

        [Authorize]
        public IActionResult ByName(string name)
        {
            var viewModel = this.eventsService.GetByName<EventViewModel>(name);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var users = this.usersService.GetAll<UserDropDownViewModel>();
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

            var eventId = await this.eventsService.CreateAsync(
                input.Title,
                input.Description,
                input.Start,
                input.End,
                input.IsAllDay,
                input.IsRecurring,
                user.Id,
                input.Color,
                input.AssignedUsersId);

            return this.Redirect("/");
        }
    }
}

using System.Threading.Tasks;
using FamilyHub.Data.Models;
using FamilyHub.Data.Models.Planner;
using FamilyHub.Services.Data.Dtos;
using FamilyHub.Services.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FamilyHub.Web.Controllers
{
    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels.Events;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        private readonly UserManager<ApplicationUser> userManager;

        public EventsController(IEventService eventService, UserManager<ApplicationUser> userManager)
        {
            this.eventService = eventService;
            this.userManager = userManager;
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
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventCreateInputModel input)
        {
            var eventToAdd = AutoMapperConfig.MapperInstance.Map<CreateEventDto>(input);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var eventId = await this.eventService.CreateAsync(eventToAdd);

            return this.Redirect("/");
        }
    }
}

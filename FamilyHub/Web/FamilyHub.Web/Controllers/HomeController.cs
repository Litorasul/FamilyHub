namespace FamilyHub.Web.Controllers
{
    using System.Diagnostics;

    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels;
    using FamilyHub.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IEventService eventService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IEventService eventService, UserManager<ApplicationUser> userManager)
        {
            this.eventService = eventService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Events =
                    this.eventService.GetAll<IndexEventViewModel>(),
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

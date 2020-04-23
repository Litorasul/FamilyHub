namespace FamilyHub.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
    using FamilyHub.Services.Data.Planner;
    using FamilyHub.Services.Data.WallPosts;
    using FamilyHub.Web.ViewModels;
    using FamilyHub.Web.ViewModels.Events;
    using FamilyHub.Web.ViewModels.WallPosts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IWallPostsService wallPostsService;
        private readonly IEventsService eventsService;

        public HomeController(
            IWallPostsService wallPostsService,
            IEventsService eventsService)
        {
            this.wallPostsService = wallPostsService;
            this.eventsService = eventsService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var viewModel = new PostsAllViewModel()
            {
                Posts = this.wallPostsService.GetAll<PostsSingleViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
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

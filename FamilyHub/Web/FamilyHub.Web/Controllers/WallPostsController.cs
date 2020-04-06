using FamilyHub.Data.Models.WallPosts;

namespace FamilyHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class WallPostsController : Controller
    {
        private readonly IWallPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public WallPostsController(IWallPostsService postsService, UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string content)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.postsService.CreateAsync(userId, PostType.StatusUpdate, null, content);
            return this.Redirect("/");
        }
    }
}
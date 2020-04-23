namespace FamilyHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FamilyHub.Data.Models;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Data;
    using FamilyHub.Services.Data.WallPosts;
    using FamilyHub.Web.ViewModels.WallPosts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class WallPostsController : Controller
    {
        private readonly IWallPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICommentsService commentsService;

        public WallPostsController(
            IWallPostsService postsService,
            UserManager<ApplicationUser> userManager,
            ICommentsService commentsService)
        {
            this.postsService = postsService;
            this.userManager = userManager;
            this.commentsService = commentsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string content)
        {
            if (content == null || string.IsNullOrEmpty(content))
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.postsService.CreateAsync(userId, PostType.StatusUpdate, null, content);
            return this.Redirect("/");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.CreateAsync(userId, input.PostId, input.Text);
            return this.Redirect("/");
        }
    }
}

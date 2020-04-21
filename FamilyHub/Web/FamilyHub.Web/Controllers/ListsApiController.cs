namespace FamilyHub.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels.Lists;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/[controller]")]
    public class ListsApiController : ControllerBase
    {
        private readonly IListsService listsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ListsApiController(IListsService listsService, UserManager<ApplicationUser> userManager)
        {
            this.listsService = listsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(ListItemUpdateDoneViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.listsService.ListItemUpdateDone(model.ItemId, userId, DateTime.UtcNow);

            return this.Ok(new { success = true });
        }
    }
}
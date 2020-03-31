namespace FamilyHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FamilyHub.Data.Models;
    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels.Lists;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ListsController : Controller
    {
        private readonly IListsService listsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ListsController(IListsService listsService, UserManager<ApplicationUser> userManager)
        {
            this.listsService = listsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult AllToDo()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult AllShopping()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult AllChores()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ListsSingleViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var listId = this.listsService.CreateAsync(model.Title, model.Description, model.Type, user.Id).Result;
            foreach (var item in model.ListItems)
            {
               await this.listsService.AddItemToList(listId, item.Text);
            }

            return this.Redirect("/");
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddListItem([Bind("ListItems")] ListsSingleViewModel lists)
        {
            lists.ListItems.Add(new ListItemViewModel());
            return this.PartialView("ListItems", lists);
        }
    }
}
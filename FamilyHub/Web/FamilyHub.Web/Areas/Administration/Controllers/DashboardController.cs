namespace FamilyHub.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly IEventsService eventsService;
        private readonly IListsService listsService;
        private readonly IPhotoAlbumsService photoAlbumsService;

        public DashboardController(
            IEventsService eventsService,
            IListsService listsService,
            IPhotoAlbumsService photoAlbumsService)
        {
            this.eventsService = eventsService;
            this.listsService = listsService;
            this.photoAlbumsService = photoAlbumsService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Events = this.eventsService.GetAllDeleted<EventsDashboardViewModel>(),
                Lists = this.listsService.GetAllDeleted<ListsDashboardViewModel>(),
                Albums = this.photoAlbumsService.GetAllDeleted<PhotoAlbumsDashboardViewModel>(),
            };
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RestoreEvent(int eventId)
        {
            await this.eventsService.UnDelete(eventId);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RestoreList(int listId)
        {
            await this.listsService.UnDelete(listId);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RestoreAlbum(int albumId)
        {
            await this.photoAlbumsService.UnDelete(albumId);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

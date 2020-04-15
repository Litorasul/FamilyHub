namespace FamilyHub.Web.Areas.Administration.Controllers
{
    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels.Administration.Dashboard;

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
    }
}

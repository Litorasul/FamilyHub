namespace FamilyHub.Web.ViewComponents
{
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels.WallPosts;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "WallPosts")]
    public class WallPostsViewComponent : ViewComponent
    {
        private readonly IListsService listsService;
        private readonly IEventsService eventsService;

        // ToDo: Add IPictureAlbumsService and IFoodService
        public WallPostsViewComponent(
            IListsService listsService,
            IEventsService eventsService)
        {
            this.listsService = listsService;
            this.eventsService = eventsService;
        }

        public IViewComponentResult Invoke(PostsSingleViewModel model)
        {
            string wallView = "Default";

            if (model.PostType == PostType.NewEvent)
            {
                var viewModel = this.eventsService.GetById<WallEventViewModel>((int)model.AssignedEntity);
                wallView = "WallEvent";

                return this.View(wallView, viewModel);
            }
            else if (model.PostType == PostType.NewList)
            {
                var viewModel = this.listsService.GetById<WallListViewModel>((int)model.AssignedEntity);
                wallView = "WallList";

                return this.View(wallView, viewModel);
            }

            return this.View(wallView, model);
        }
    }
}
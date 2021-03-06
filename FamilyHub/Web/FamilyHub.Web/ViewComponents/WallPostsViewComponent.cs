﻿namespace FamilyHub.Web.ViewComponents
{
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Data;
    using FamilyHub.Services.Data.Lists;
    using FamilyHub.Services.Data.PhotoAlbum;
    using FamilyHub.Services.Data.Planner;
    using FamilyHub.Web.ViewModels.WallPosts;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "WallPosts")]
    public class WallPostsViewComponent : ViewComponent
    {
        private readonly IListsService listsService;
        private readonly IEventsService eventsService;
        private readonly IPhotoAlbumsService photoAlbumsService;

        // ToDo: Add IFoodService
        public WallPostsViewComponent(
            IListsService listsService,
            IEventsService eventsService,
            IPhotoAlbumsService photoAlbumsService)
        {
            this.listsService = listsService;
            this.eventsService = eventsService;
            this.photoAlbumsService = photoAlbumsService;
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
            else if (model.PostType == PostType.NewPicture)
            {
                var viewModel = this.photoAlbumsService.GetById<WallPictureAlbumViewModel>((int) model.AssignedEntity);
                wallView = "WallPictureAlbum";

                return this.View(wallView, viewModel);
            }

            return this.View(wallView, model);
        }
    }
}
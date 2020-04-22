namespace FamilyHub.Services.Data.Tests.WallPosts
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Web.ViewModels.WallPosts;

    public class PostsSingleViewModelBuilder
    {
        public static PostsSingleViewModel EventModel => NewEventViewModelBuilder();

        public static PostsSingleViewModel ListModel => NewListViewModelBuilder();

        public static PostsSingleViewModel PhotoModel => NewPictureViewModelBuilder();

        public static PostsSingleViewModel StatusUpdateModel => StatusUpdateViewModelBuilder();

        private static PostsSingleViewModel NewEventViewModelBuilder()
        {
            var model = new PostsSingleViewModel
            {
                Id = 1,
                PostType = PostType.NewEvent,
                AssignedEntity = 1,
                Comments = new List<CommentViewModel>(),
                CreatedOn = DateTime.Today,
                UserUserName = "aaa",
                UserProfilePictureUrl = "bbb",
            };

            return model;
        }

        private static PostsSingleViewModel NewListViewModelBuilder()
        {
            var model = new PostsSingleViewModel
            {
                Id = 2,
                PostType = PostType.NewList,
                AssignedEntity = 2,
                Comments = new List<CommentViewModel>(),
                CreatedOn = DateTime.Today,
                UserUserName = "ccc",
                UserProfilePictureUrl = "ddd",
            };

            return model;
        }

        private static PostsSingleViewModel NewPictureViewModelBuilder()
        {
            var model = new PostsSingleViewModel
            {
                Id = 3,
                PostType = PostType.NewPicture,
                AssignedEntity = 3,
                Comments = new List<CommentViewModel>(),
                CreatedOn = DateTime.Today,
                UserUserName = "eee",
                UserProfilePictureUrl = "fff",
            };

            return model;
        }

        private static PostsSingleViewModel StatusUpdateViewModelBuilder()
        {
            var model = new PostsSingleViewModel
            {
                Id = 4,
                PostType = PostType.StatusUpdate,
                AssignedEntity = 4,
                Comments = new List<CommentViewModel>(),
                CreatedOn = DateTime.Today,
                UserUserName = "ggg",
                UserProfilePictureUrl = "hhh",
            };

            return model;
        }
    }
}

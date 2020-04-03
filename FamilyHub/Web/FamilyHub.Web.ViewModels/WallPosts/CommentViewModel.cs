namespace FamilyHub.Web.ViewModels.WallPosts
{
    using System;

    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public string Text { get; set; }
    }
}
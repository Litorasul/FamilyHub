namespace FamilyHub.Web.ViewModels.WallPosts
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Mapping;

    public class PostsSingleViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public PostType PostType { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? AssignedEntity { get; set; }

        public string Content { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
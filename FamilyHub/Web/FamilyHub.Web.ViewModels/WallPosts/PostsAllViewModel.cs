namespace FamilyHub.Web.ViewModels.WallPosts
{
    using System.Collections.Generic;

    public class PostsAllViewModel
    {
        public IEnumerable<PostsSingleViewModel> Posts { get; set; }
    }
}
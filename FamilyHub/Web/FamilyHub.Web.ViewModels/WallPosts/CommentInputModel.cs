namespace FamilyHub.Web.ViewModels.WallPosts
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        public int PostId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
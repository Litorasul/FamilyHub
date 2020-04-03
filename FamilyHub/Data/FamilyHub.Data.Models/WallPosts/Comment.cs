using FamilyHub.Data.Common.Models;

namespace FamilyHub.Data.Models.WallPosts
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment : BaseDeletableModel<int>
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public string Text { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
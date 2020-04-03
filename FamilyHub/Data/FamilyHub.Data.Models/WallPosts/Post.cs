namespace FamilyHub.Data.Models.WallPosts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
        }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public PostType PostType { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
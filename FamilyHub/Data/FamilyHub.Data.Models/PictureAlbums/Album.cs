namespace FamilyHub.Data.Models.PictureAlbums
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    using static FamilyHub.Data.Models.DataValidation;

    public class Album : BaseDeletableModel<int>
    {
        public Album()
        {
            this.Pictures = new HashSet<Picture>();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string Location { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
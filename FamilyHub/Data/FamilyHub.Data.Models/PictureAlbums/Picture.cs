namespace FamilyHub.Data.Models.PictureAlbums
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    public class Picture : BaseDeletableModel<int>
    {
        public Picture()
        {
            this.Users = new HashSet<UserPicture>();
        }

        [Required]
        public string Url { get; set; }

        public string ThumbUrl { get; set; }

        public virtual ICollection<UserPicture> Users { get; set; }

        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
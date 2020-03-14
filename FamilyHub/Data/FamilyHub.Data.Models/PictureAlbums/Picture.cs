﻿namespace FamilyHub.Data.Models.PictureAlbums
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        public Picture()
        {
            this.Users = new HashSet<UserPicture>();
        }

        [Required]
        public string Url { get; set; }

        public ICollection<UserPicture> Users { get; set; }
    }
}
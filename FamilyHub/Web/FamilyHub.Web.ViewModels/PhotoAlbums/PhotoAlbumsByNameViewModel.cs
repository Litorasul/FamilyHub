namespace FamilyHub.Web.ViewModels.PhotoAlbums
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Services.Mapping;

    public class PhotoAlbumsByNameViewModel : IMapFrom<Album>
    {
        public PhotoAlbumsByNameViewModel()
        {
            this.Pictures = new HashSet<PictureInAlbumViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<PictureInAlbumViewModel> Pictures { get; set; }
    }
}

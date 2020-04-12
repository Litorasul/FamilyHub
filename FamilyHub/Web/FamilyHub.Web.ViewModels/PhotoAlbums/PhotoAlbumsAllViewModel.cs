namespace FamilyHub.Web.ViewModels.PhotoAlbums
{
    using System.Collections.Generic;

    public class PhotoAlbumsAllViewModel
    {
        public IEnumerable<PhotoAlbumSingleViewModel> Albums { get; set; }
    }
}
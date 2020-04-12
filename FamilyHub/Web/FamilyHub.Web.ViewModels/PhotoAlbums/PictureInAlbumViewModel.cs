namespace FamilyHub.Web.ViewModels.PhotoAlbums
{
    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Services.Mapping;

    public class PictureInAlbumViewModel : IMapFrom<Picture>
    {
        public string Url { get; set; }

        public string ThumbUrl { get; set; }
    }
}

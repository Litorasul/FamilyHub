namespace FamilyHub.Web.ViewModels.Tests
{
    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Services.Mapping;

    public class TestAlbumViewModel : IMapFrom<Album>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}

namespace FamilyHub.Web.ViewModels.WallPosts
{
    using System.Linq;

    using AutoMapper;
    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Services.Mapping;

    public class WallPictureAlbumViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url => $"/Photos/{this.Title.Replace(' ', '-')}";

        public string PictureThumb { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Album, WallPictureAlbumViewModel>()
                .ForMember(
                    x => x.PictureThumb,
                    c
                        => c.MapFrom(e => e.Pictures.FirstOrDefault().ThumbUrl));
        }
    }
}
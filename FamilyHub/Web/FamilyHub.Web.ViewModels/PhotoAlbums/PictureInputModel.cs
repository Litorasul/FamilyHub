namespace FamilyHub.Web.ViewModels.PhotoAlbums
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class PictureInputModel
    {
        [Required]
        public int AlbumId { get; set; }

        [Required]
        public string AlbumName { get; set; }

        [Required]
        [FileExtensions]
        public IFormFile File { get; set; }
    }
}

namespace FamilyHub.Web.ViewModels.PhotoAlbums
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreatePhotoAlbumInputModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [FileExtensions]
        public IFormFile Picture { get; set; }
    }
}

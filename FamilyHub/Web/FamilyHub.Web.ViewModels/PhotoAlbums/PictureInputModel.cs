namespace FamilyHub.Web.ViewModels.PhotoAlbums
{
    using System.ComponentModel.DataAnnotations;

    public class PictureInputModel
    {
        [Required]
        public int AlbumId { get; set; }

        [Required]
        public string AlbumName { get; set; }

        [Required]
        [FileExtensions]
        public byte[] File { get; set; }
    }
}

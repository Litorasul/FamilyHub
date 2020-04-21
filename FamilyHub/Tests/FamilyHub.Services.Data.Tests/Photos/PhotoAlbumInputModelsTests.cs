namespace FamilyHub.Services.Data.Tests.Photos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;

    using FamilyHub.Web.ViewModels.PhotoAlbums;
    using Microsoft.AspNetCore.Http;
    using Xunit;

    public class PhotoAlbumInputModelsTests
    {
        [Fact]
        public void CreatePhotoAlbumInputModelShouldHaveTitle()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");

            var album = new CreatePhotoAlbumInputModel()
            {
                Title = null,
                Description = "ddd",
                Picture = file,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(album, new ValidationContext(album), validatorResults, true);

            Assert.False(actual);
        }
    }
}

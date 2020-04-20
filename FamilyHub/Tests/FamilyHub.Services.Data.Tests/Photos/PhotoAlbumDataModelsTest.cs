namespace FamilyHub.Services.Data.Tests.Photos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Models.PictureAlbums;
    using Xunit;

    public class PhotoAlbumDataModelsTest
    {
        [Fact]
        public void AlbumShouldHaveTitle()
        {
            var album = new Album()
            {
                Title = null,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(album, new ValidationContext(album), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void PictureShouldHaveUrl()
        {
            var picture = new Picture
            {
                Url = null,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(picture, new ValidationContext(picture), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }
    }
}
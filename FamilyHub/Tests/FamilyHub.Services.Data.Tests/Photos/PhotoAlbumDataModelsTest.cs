namespace FamilyHub.Services.Data.Tests.Photos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data;
    using FamilyHub.Data.Models.PictureAlbums;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PhotoAlbumDataModelsTest : IDisposable
    {
        private readonly ApplicationDbContext dbContext;

        public PhotoAlbumDataModelsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

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

        [Fact]
        public void UserPictureShouldHaveUserId()
        {
            var userPicture = new UserPicture
            {
                UserId = null,
                PictureId = 1,
            };

            Assert.Throws<InvalidOperationException>(
                () => this.dbContext.UserPictures.Add(userPicture));
        }
    }
}

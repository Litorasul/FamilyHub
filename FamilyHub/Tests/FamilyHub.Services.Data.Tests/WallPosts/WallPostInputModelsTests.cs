namespace FamilyHub.Services.Data.Tests.WallPosts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Web.ViewModels.WallPosts;
    using Xunit;

    public class WallPostInputModelsTests
    {
        [Fact]
        public void CommentInputModelShouldHaveText()
        {
            var comment = new CommentInputModel()
            {
                PostId = 1,
                Text = null,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(comment, new ValidationContext(comment), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }
    }
}
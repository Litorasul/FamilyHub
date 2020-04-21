using FamilyHub.Web.ViewModels.WallPosts;

namespace FamilyHub.Services.Data.Tests.WallPosts
{
    using FamilyHub.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class WallPostsControllerTests
    {
        [Fact]
        public void CreateShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<WallPostsController>
                .Calling(c => c.Create(With.Any<string>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void CreateCommentShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<WallPostsController>
                .Calling(c => c.CreateComment(With.Default<CommentInputModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}

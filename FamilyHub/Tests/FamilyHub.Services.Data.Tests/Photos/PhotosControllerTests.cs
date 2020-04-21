namespace FamilyHub.Services.Data.Tests.Photos
{
    using FamilyHub.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class PhotosControllerTests
    {
        [Fact]
        public void AllAlbumsShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<PhotosController>
                .Calling(c => c.AllAlbums())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void ByNameShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<PhotosController>
                .Calling(c => c.ByName(With.Any<string>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void CreateAlbumGetShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<PhotosController>
                .Calling(c => c.CreateAlbum())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void DeleteAlbumShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<PhotosController>
                .Calling(c => c.DeleteAlbum(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}

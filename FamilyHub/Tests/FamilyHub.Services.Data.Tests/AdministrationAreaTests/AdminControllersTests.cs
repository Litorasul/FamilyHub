namespace FamilyHub.Services.Data.Tests.AdministrationAreaTests
{
    using FamilyHub.Common;
    using FamilyHub.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AdminControllersTests
    {
        [Fact]
        public void AdministrationControllerShouldBeInAdminArea()
            => MyController<AdministrationController>
                .ShouldHave()
                .Attributes(attrs => attrs
                    .SpecifyingArea("Administration")
                    .RestrictingForAuthorizedRequests(GlobalConstants.AdministratorRoleName));

        [Fact]
        public void IndexShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<DashboardController>
                .Calling(c => c.Index())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void RestoreEventShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<DashboardController>
                .Calling(c => c.RestoreEvent(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void RestoreListShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<DashboardController>
                .Calling(c => c.RestoreList(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void RestoreAlbumShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<DashboardController>
                .Calling(c => c.RestoreAlbum(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}
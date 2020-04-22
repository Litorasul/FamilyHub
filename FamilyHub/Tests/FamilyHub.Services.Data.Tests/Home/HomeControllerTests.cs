namespace FamilyHub.Services.Data.Tests.Home
{
    using FamilyHub.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<HomeController>
                .Calling(c => c.Index())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void PrivacyShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<HomeController>
                .Calling(c => c.Privacy())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
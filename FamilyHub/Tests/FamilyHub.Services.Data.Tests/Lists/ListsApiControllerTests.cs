namespace FamilyHub.Services.Data.Tests.Lists
{
    using FamilyHub.Web.Controllers;
    using FamilyHub.Web.ViewModels.Lists;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ListsApiControllerTests
    {
        [Fact]
        public void PostShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnOk()
            => MyController<ListsApiController>
                .Calling(c => c.Post(With.Default<ListItemUpdateDoneViewModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests()
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post))
                .AndAlso()
                .ShouldReturn()
                .Ok();
    }
}
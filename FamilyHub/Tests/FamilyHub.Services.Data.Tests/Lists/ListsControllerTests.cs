namespace FamilyHub.Services.Data.Tests.Lists
{
    using FamilyHub.Web.Controllers;
    using FamilyHub.Web.ViewModels.Lists;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ListsControllerTests
    {
        [Fact]
        public void AllToDoShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<ListsController>
                .Calling(c => c.AllToDo())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void AllShoppingShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<ListsController>
                .Calling(c => c.AllShopping())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void AllChoresShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<ListsController>
                .Calling(c => c.AllChores())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void ByNameShouldHaveAuthorizedUsersOnlyRestriction()
            => MyController<ListsController>
                .Calling(c => c.ByName("lll"))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void CreateGetShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<ListsController>
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void CreatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<ListsController>
                .Calling(c => c.Create(With.Default<ListCreateInputModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void AddListItemShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<ListsController>
                .Calling(c => c.AddListItem(With.Default<ListsSingleViewModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void UpdateAddNewListItemShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<ListsController>
                .Calling(c => c.UpdateAddNewListItem(With.Default<ListUpdateViewModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void DeleteListShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<ListsController>
                .Calling(c => c.DeleteList(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}

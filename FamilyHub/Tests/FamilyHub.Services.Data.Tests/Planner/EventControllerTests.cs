namespace FamilyHub.Services.Data.Tests.Planner
{
    using FamilyHub.Services.Mapping;
    using FamilyHub.Web.Controllers;
    using FamilyHub.Web.ViewModels.Events;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class EventControllerTests
    {
        public EventControllerTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(EventViewModel).Assembly);
        }

        [Fact]
        public void AllShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<EventsController>
                .Calling(c => c.All())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void CalendarShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<EventsController>
                .Calling(c => c.Calendar())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void GetEventsShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnJson()
            => MyController<EventsController>
                .Calling(c => c.GetEvents())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Json();

        [Fact]
        public void ByNameShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<EventsController>
                .Calling(c => c.ByName(With.Any<string>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void CreateGetShouldHaveAuthorizedUsersOnlyRestrictionAndShouldReturnView()
            => MyController<EventsController>
                .Calling(c => c.Create())
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void CreatePostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<EventsController>
                .Calling(c => c.Create(With.Default<EventCreateInputModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void EditGetShouldHaveAuthorizedUsersOnlyRestriction()
            => MyController<EventsController>
                .Calling(c => c.Edit(1))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void EditPostShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<EventsController>
                .Calling(c => c.Edit(With.Default<EventUpdateViewModel>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void DeleteEventShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<EventsController>
                .Calling(c => c.DeleteEvent(With.Any<int>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(System.Net.Http.HttpMethod.Post)
                    .RestrictingForAuthorizedRequests());
    }
}

namespace FamilyHub.Services.Data.Tests.Planner
{
    using FamilyHub.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class EventsRoutingTests
    {
        [Fact]
        public void ByNameShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Events/SomeName")
                .To<EventsController>(c => c.ByName("SomeName"));
    }
}
namespace FamilyHub.Services.Data.Tests.Lists
{
    using FamilyHub.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ListRoutingTests
    {
        [Fact]
        public void ByNameShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Lists/SomeName")
                .To<ListsController>(c => c.ByName("SomeName"));
    }
}
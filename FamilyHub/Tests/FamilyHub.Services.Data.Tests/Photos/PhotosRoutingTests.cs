namespace FamilyHub.Services.Data.Tests.Photos
{
    using FamilyHub.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class PhotosRoutingTests
    {
        [Fact]
        public void ByNameShouldBeRoutedCorrectly()
            => MyRouting
                .Configuration()
                .ShouldMap("/Photos/SomeName")
                .To<PhotosController>(c => c.ByName("SomeName"));
    }
}
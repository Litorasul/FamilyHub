namespace FamilyHub.Services.Data.Tests.WallPosts
{
    using FamilyHub.Web.ViewComponents;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class WallPostsViewComponentTests
    {
        [Fact]
        public void NewEventTypeModelShouldReturnWallEventView()
            => MyViewComponent<WallPostsViewComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(PostsSingleViewModelBuilder.EventModel))
                .ShouldReturn()
                .View("WallEvent");

        [Fact]
        public void NewListTypeModelShouldReturnWallListView()
            => MyViewComponent<WallPostsViewComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(PostsSingleViewModelBuilder.ListModel))
                .ShouldReturn()
                .View("WallList");

        [Fact]
        public void NewPictureTypeModelShouldReturnWallPictureAlbumView()
            => MyViewComponent<WallPostsViewComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(PostsSingleViewModelBuilder.PhotoModel))
                .ShouldReturn()
                .View("WallPictureAlbum");

        [Fact]
        public void StatusUpdateTypeModelShouldReturnDefaultView()
            => MyViewComponent<WallPostsViewComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(PostsSingleViewModelBuilder.StatusUpdateModel))
                .ShouldReturn()
                .View("Default");
    }
}

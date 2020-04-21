namespace FamilyHub.Web.ViewModels.Tests
{
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Mapping;

    public class TestPostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }
    }
}
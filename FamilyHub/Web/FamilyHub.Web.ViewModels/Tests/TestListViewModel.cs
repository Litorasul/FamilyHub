namespace FamilyHub.Web.ViewModels.Tests
{
    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Services.Mapping;

    public class TestListViewModel : IMapFrom<List>
    {
        public int Id { get; set; }

        public ListType Type { get; set; }

        public string Title { get; set; }
    }
}

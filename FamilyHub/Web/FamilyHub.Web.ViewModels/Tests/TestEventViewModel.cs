namespace FamilyHub.Web.ViewModels.Tests
{
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class TestEventViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
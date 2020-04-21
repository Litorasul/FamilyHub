namespace FamilyHub.Web.ViewModels.Tests
{
    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class TestConversationViewModel : IMapFrom<Conversation>
    {
        public string Name { get; set; }
    }
}
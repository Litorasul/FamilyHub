namespace FamilyHub.Web.ViewModels.Tests
{
    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class TestMessageViewModel : IMapFrom<Message>
    {
        public string Text { get; set; }

        public string UserId { get; set; }
    }
}

namespace FamilyHub.Web.ViewModels.Messenger
{
    using System.Collections.Generic;

    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Services.Mapping;

    public class ConversationViewModel : IMapFrom<Conversation>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<string> UserId { get; set; }

        public ICollection<string> UserUserName { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }

    }
}
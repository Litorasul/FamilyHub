namespace FamilyHub.Web.ViewModels.Messenger
{
    using System.Collections.Generic;

    public class MessagesPerConversationViewModel
    {

        public int ConversationId { get; set; }

        public string Name { get; set; }

        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}

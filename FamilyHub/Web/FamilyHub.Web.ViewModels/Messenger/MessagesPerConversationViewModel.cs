namespace FamilyHub.Web.ViewModels.Messenger
{
    using System.Collections.Generic;

    public class MessagesPerConversationViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}

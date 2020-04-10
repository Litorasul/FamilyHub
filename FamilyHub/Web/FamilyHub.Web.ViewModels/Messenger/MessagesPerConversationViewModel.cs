namespace FamilyHub.Web.ViewModels.Messenger
{
    using System.Collections.Generic;

    public class MessagesPerConversationViewModel
    {
        public string Name { get; set; }

        public IEnumerable<MessageViewModel> Messages { get; set; }
    }
}

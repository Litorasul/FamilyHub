namespace FamilyHub.Data.Models.Messenger
{
    public class UserConversation
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ConversationId { get; set; }

        public Conversation Conversation { get; set; }
    }
}
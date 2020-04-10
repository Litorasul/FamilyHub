namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyHub.Data.Models.Messenger;

    public interface IMessengerService
    {
        IEnumerable<T> GetAllConversation<T>(string userId, int? count = null);

        IEnumerable<T> GetAllMessagesForConversation<T>(int conversationId);

        string GetConversationNameById(int conversationId);

        Task<Message> AddMessage(string userId, int conversationId, string text);
    }
}

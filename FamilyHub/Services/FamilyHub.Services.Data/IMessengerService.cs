namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyHub.Data.Models.Messenger;

    public interface IMessengerService
    {
        IEnumerable<T> GetAllConversation<T>(string userId, int? count = null);

        IEnumerable<T> GetAllMessages<T>();

        string GetConversationNameById(int conversationId);

        Task<T> AddMessage<T>(string userId, string text);
    }
}

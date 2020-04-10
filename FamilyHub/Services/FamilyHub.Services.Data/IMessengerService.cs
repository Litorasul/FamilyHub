namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;

    public interface IMessengerService
    {
        IEnumerable<T> GetAllConversation<T>(string userId, int? count = null);
    }
}
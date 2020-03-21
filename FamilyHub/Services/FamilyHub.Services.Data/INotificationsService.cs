namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyHub.Data.Models.Notification;

    public interface INotificationsService
    {
        Task<int> CreateNotificationAsync(NotificationType type, int typeId, string userId);

        Task<int> CreateGroupNotificationAsync(NotificationType type, int typeId, IEnumerable<string> userIds);

        IEnumerable<T> GetAllByUser<T>(string userId, int? count = null);
    }
}

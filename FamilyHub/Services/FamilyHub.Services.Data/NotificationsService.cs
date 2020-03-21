using System;

namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.Notification;
    using FamilyHub.Services.Mapping;

    public class NotificationsService : INotificationsService
    {
        private readonly IDeletableEntityRepository<Notification> notificationRepository;

        public NotificationsService(IDeletableEntityRepository<Notification> notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Create a single Notification.
        /// </summary>
        /// <returns>The Id of the created Notification.</returns>
        public async Task<int> CreateNotificationAsync(NotificationType type, int typeId, string userId)
        {
            string message = $"You have new {type.ToString()}";

            var notification = new Notification
            {
                Message = message,
                NotificationType = type,
                NotificationTypeId = typeId,
                UserId = userId,
            };
            await this.notificationRepository.AddAsync(notification);
            await this.notificationRepository.SaveChangesAsync();

            return notification.Id;
        }

        /// <summary>
        /// Creates Notifications for group of Assigned Users when creating new entity.
        /// </summary>
        /// <param name="userIds"> The Ids of the assigned users. </param>
        /// <returns> How much Notifications have been created. </returns>
        public async Task<int> CreateGroupNotificationAsync(NotificationType type, int typeId, IEnumerable<string> userIds)
        {
            int notificationsCreated = 0;

            foreach (var userId in userIds)
            {
                await this.CreateNotificationAsync(type, typeId, userId);
                notificationsCreated++;
            }

            return notificationsCreated;
        }

        public IEnumerable<T> GetAllByUser<T>(string userId, int? count = null)
        {
            IQueryable<Notification> query = this.notificationRepository
                .All()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.CreatedOn);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
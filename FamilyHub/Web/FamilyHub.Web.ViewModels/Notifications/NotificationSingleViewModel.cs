namespace FamilyHub.Web.ViewModels.Notifications
{
    using System;

    using FamilyHub.Data.Models.Notification;
    using FamilyHub.Services.Mapping;

    public class NotificationSingleViewModel : IMapFrom<Notification>
    {
        public DateTime CreatedOn { get; set; }

        public NotificationType NotificationType { get; set; }

        public int NotificationTypeId { get; set; }

        public string Message { get; set; }
    }
}
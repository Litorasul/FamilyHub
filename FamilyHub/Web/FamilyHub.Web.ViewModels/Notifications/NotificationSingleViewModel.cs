namespace FamilyHub.Web.ViewModels.Notifications
{
    using System;

    using AutoMapper;
    using FamilyHub.Data.Models.Notification;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class NotificationSingleViewModel : IMapFrom<Notification>
    {
        public DateTime CreatedOn { get; set; }

        public NotificationType NotificationType { get; set; }

        public int NotificationTypeId { get; set; }

        public string Message { get; set; }

        public string NotificationTypeTitle { get; set; }

        public string NotificationTypeDescription { get; set; }

    }
}

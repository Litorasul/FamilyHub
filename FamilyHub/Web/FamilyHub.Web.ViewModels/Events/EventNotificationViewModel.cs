namespace FamilyHub.Web.ViewModels.Events
{
    using System;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class EventNotificationViewModel : IMapFrom<Event>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
    }
}
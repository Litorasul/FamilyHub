namespace FamilyHub.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class EventViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public TimeSpan? Duration { get; set; }

        public string CreatorUserName { get; set; }

        public string CreatorId { get; set; }

        public IEnumerable<AssignedUsersViewModel> AssignedUsers { get; set; }
    }
}

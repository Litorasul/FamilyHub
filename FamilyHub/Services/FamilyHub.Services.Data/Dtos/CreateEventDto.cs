namespace FamilyHub.Services.Data.Dtos
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class CreateEventDto : IMapTo<Event>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public bool IsFullDayEvent { get; set; }

        public bool IsRecurring { get; set; }

        public IEnumerable<string> AssignedUsersId { get; set; }
    }
}
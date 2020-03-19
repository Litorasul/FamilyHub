using FamilyHub.Services.Mapping;

namespace FamilyHub.Services.Data.Dtos
{
    using System;
    using System.Collections.Generic;

    public class CreateEventDto
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
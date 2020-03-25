namespace FamilyHub.Web.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class EventCalendarViewModel : IMapFrom<Event>
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets start time of the Event. Required.
        /// </summary>
        [Required]
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets end time of the Event. Not Required.
        /// </summary>
        public DateTime? End { get; set; }

        public bool IsAllDay { get; set; }

        public string Color { get; set; }
    }
}
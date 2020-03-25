namespace FamilyHub.Data.Models.Planner
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    using static FamilyHub.Data.Models.DataValidation;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.AssignedUsers = new HashSet<UserEvent>();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(DescriptionMaxLength)]
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

        public bool IsRecurring { get; set; }

        /// <summary>
        /// Gets or sets the color of the Event in Hex. Nor Required.
        /// </summary>
        public string Color { get; set; }

        [Required]
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<UserEvent> AssignedUsers { get; set; }
    }
}

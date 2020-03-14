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

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime{ get; set; }

        public bool IsFullDayEvent { get; set; }

        public bool IsRecurring { get; set; }

        [Required]
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public ICollection<UserEvent> AssignedUsers { get; set; }
    }
}
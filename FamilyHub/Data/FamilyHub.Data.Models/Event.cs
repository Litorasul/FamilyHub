namespace FamilyHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.AssignedUsers = new HashSet<UserEvent>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
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
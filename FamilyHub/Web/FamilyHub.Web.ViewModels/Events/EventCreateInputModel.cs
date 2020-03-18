namespace FamilyHub.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static FamilyHub.Data.Models.DataValidation;

    public class EventCreateInputModel
    {
        public EventCreateInputModel()
        {
            this.AssignedUsersId = new HashSet<string>();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Display(Name = "Full Day Event")]
        public bool IsFullDayEvent { get; set; }

        [Display(Name = "Recurring")]
        public bool IsRecurring { get; set; }

        [Display(Name = "For Family-members:")]
        public IEnumerable<string> AssignedUsersId { get; set; }
    }
}
namespace FamilyHub.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Web.ViewModels.Users;

    using static FamilyHub.Data.Models.DataValidation;

    public class EventCreateInputModel

    {
        public EventCreateInputModel()
        {
            this.AssignedUsersId = new HashSet<string>();
        }

        [Required] [MaxLength(TitleMaxLength)] 
        public string Title { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required] 
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [Display(Name = "Full Day Event")]
        public bool IsAllDay { get; set; }

        [Display(Name = "Recurring")]
        public bool IsRecurring { get; set; }

        [Display(Name = "For Family-members:")]
        public IEnumerable<string> AssignedUsersId { get; set; }

        public IEnumerable<UserDropDownViewModel> Users { get; set; }
    }
}

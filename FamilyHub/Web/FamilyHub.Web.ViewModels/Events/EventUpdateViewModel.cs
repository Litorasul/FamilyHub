namespace FamilyHub.Web.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    using static FamilyHub.Data.Models.DataValidation;

    public class EventUpdateViewModel : IMapFrom<Event>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
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

        public string Color { get; set; }
    }
}

namespace FamilyHub.Data.Models.Survey
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;
    using FamilyHub.Data.Models.Planner;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    using static FamilyHub.Data.Models.DataValidation;

    public class Survey : BaseDeletableModel<int>
    {
        public Survey()
        {
            this.Questions = new HashSet<SurveyQuestion>();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime ExpiresOn { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<SurveyQuestion> Questions { get; set; }
    }
}
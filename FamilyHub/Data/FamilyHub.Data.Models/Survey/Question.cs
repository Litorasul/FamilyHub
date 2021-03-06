﻿namespace FamilyHub.Data.Models.Survey
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Common.Models;

    using static FamilyHub.Data.Models.DataValidation;

    public class Question : BaseDeletableModel<int>
    {
        public Question()
        {
            this.Surveys = new HashSet<SurveyQuestion>();
            this.Answers = new HashSet<Answer>();
            this.Responses = new HashSet<Response>();
        }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<SurveyQuestion> Surveys { get; set; }

        public virtual ICollection<Response> Responses { get; set; }
    }
}
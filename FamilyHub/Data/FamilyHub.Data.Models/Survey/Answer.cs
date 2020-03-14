namespace FamilyHub.Data.Models.Survey
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    using static FamilyHub.Data.Models.DataValidation;

    public class Answer : BaseDeletableModel<int>
    {
        public Answer()
        {
            this.Responses = new HashSet<Response>();
        }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        public ICollection<Response> Responses { get; set; }
    }
}
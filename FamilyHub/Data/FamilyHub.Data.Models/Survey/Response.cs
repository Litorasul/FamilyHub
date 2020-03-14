namespace FamilyHub.Data.Models.Survey
{
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    public class Response : BaseDeletableModel<int>
    {
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        [ForeignKey("Answer")]
        public int AnswerId { get; set; }

        public Answer Answer { get; set; }
    }
}
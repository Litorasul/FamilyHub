namespace FamilyHub.Data.Models.Survey
{
    public class SurveyQuestion
    {
        public int SurveyId { get; set; }

        public virtual Survey Survey { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
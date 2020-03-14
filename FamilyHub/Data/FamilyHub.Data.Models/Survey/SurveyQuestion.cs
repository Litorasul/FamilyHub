namespace FamilyHub.Data.Models.Survey
{
    public class SurveyQuestion
    {
        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
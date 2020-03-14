namespace FamilyHub.Data.Configurations
{
    using FamilyHub.Data.Models.Survey;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
    {
        public void Configure(EntityTypeBuilder<SurveyQuestion> surveyQuestion)
        {
            surveyQuestion
                .HasKey(sq => new { sq.SurveyId, sq.QuestionId });

            surveyQuestion
                .HasOne(sq => sq.Survey)
                .WithMany(s => s.Questions)
                .HasForeignKey(sq => sq.SurveyId)
                .OnDelete(DeleteBehavior.Restrict);

            surveyQuestion
                .HasOne(sq => sq.Question)
                .WithMany(q => q.Surveys)
                .HasForeignKey(sq => sq.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
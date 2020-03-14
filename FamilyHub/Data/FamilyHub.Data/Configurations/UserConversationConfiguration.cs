namespace FamilyHub.Data.Configurations
{
    using FamilyHub.Data.Models.Messenger;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConversationConfiguration : IEntityTypeConfiguration<UserConversation>
    {
        public void Configure(EntityTypeBuilder<UserConversation> userConversation)
        {
            userConversation
                .HasKey(uc => new { uc.UserId, uc.ConversationId });

            userConversation
                .HasOne(uc => uc.User)
                .WithMany(u => u.Conversations)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            userConversation
                .HasOne(uc => uc.Conversation)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
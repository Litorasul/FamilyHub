namespace FamilyHub.Data.Configurations
{
    using FamilyHub.Data.Models;
    using FamilyHub.Data.Models.Planner;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> userEvent)
        {
            userEvent
                .HasKey(ue => new { ue.UserId, ue.EventId });

            userEvent
                .HasOne(ue => ue.User)
                .WithMany(u => u.AssignedEvents)
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            userEvent
                .HasOne(ue => ue.Event)
                .WithMany(e => e.AssignedUsers)
                .HasForeignKey(ue => ue.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
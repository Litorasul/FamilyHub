namespace FamilyHub.Data.Configurations
{
    using FamilyHub.Data.Models.Lists;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserListConfiguration : IEntityTypeConfiguration<UserList>
    {
        public void Configure(EntityTypeBuilder<UserList> userList)
        {
            userList
                .HasKey(ul => new { ul.UserId, ul.ListId });

            userList
                .HasOne(ul => ul.User)
                .WithMany(u => u.AssignedLists)
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            userList
                .HasOne(ul => ul.List)
                .WithMany(l => l.AssignedUsers)
                .HasForeignKey(ul => ul.ListId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
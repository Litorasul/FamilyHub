namespace FamilyHub.Data.Configurations
{
    using FamilyHub.Data.Models.PictureAlbums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserPictureConfiguration : IEntityTypeConfiguration<UserPicture>
    {
        public void Configure(EntityTypeBuilder<UserPicture> userPicture)
        {
            userPicture
                .HasKey(up => new { up.UserId, up.PictureId });

            userPicture
                .HasOne(up => up.User)
                .WithMany(u => u.Pictures)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            userPicture
                .HasOne(up => up.Picture)
                .WithMany(p => p.Users)
                .HasForeignKey(up => up.PictureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
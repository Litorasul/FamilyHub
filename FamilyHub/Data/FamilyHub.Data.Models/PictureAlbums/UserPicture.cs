namespace FamilyHub.Data.Models.PictureAlbums
{
    public class UserPicture
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
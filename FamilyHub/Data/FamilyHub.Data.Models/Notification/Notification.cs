namespace FamilyHub.Data.Models.Notification
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    public class Notification : BaseDeletableModel<int>
    {
        [Required]
        public NotificationType NotificationType { get; set; }

        public int NotificationTypeId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool UserViewed { get; set; }
    }
}
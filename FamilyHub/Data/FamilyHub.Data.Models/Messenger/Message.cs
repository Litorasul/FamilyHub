namespace FamilyHub.Data.Models.Messenger
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;

    using static FamilyHub.Data.Models.DataValidation;

    public class Message : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(MessageMaxLength)]
        public string Text { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
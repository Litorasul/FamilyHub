namespace FamilyHub.Data.Models.Lists
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    using static FamilyHub.Data.Models.DataValidation;

    public class ListItem : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        public double? Amount { get; set; }

        public DateTime? DueDateTime { get; set; }

        public DateTime? DoneDateTime { get; set; }

        [ForeignKey("List")]
        public int ListId { get; set; }

        public List List { get; set; }

        [ForeignKey("DoneByUser")]
        public string DoneByUserId { get; set; }

        public virtual ApplicationUser DoneByUser { get; set; }
    }
}
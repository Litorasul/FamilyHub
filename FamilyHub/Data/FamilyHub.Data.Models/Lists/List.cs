namespace FamilyHub.Data.Models.Lists
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using FamilyHub.Data.Common.Models;
    using FamilyHub.Data.Models.Planner;

    using static FamilyHub.Data.Models.DataValidation;

    public class List : BaseDeletableModel<int>
    {
        public List()
        {
            this.AssignedUsers = new HashSet<UserList>();
            this.ListItems = new HashSet<ListItem>();
        }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public ListType Type { get; set; }

        public DateTime? DueDate { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<UserList> AssignedUsers { get; set; }

        public virtual ICollection<ListItem> ListItems { get; set; }
    }
}
namespace FamilyHub.Web.ViewModels.Lists
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Models.Lists;

    public class ListCreateInputModel
    {
        public ListCreateInputModel()
        {
            this.ListItems = new HashSet<ListItemViewModel>();
        }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public ListType Type { get; set; }

        public DateTime? DueDate { get; set; }

        public ICollection<ListItemViewModel> ListItems { get; set; }
    }
}
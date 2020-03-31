namespace FamilyHub.Web.ViewModels.Lists
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Services.Mapping;

    public class ListsSingleViewModel : IMapFrom<List>
    {
        public ListsSingleViewModel()
        {
            this.ListItems = new HashSet<ListItemViewModel>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public ListType Type { get; set; }

        public DateTime? DueDate { get; set; }

        public ICollection<ListItemViewModel> ListItems { get; set; }
    }
}
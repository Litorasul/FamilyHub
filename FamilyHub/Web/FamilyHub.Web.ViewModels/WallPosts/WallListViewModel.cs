namespace FamilyHub.Web.ViewModels.WallPosts
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Services.Mapping;
    using FamilyHub.Web.ViewModels.Lists;

    public class WallListViewModel : IMapFrom<List>
    {
        public WallListViewModel()
        {
            this.ListItems = new HashSet<ListItemViewModel>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public ListType Type { get; set; }

        public string Url => $"/Lists/{this.Title.Replace(' ', '-')}";

        public ICollection<ListItemViewModel> ListItems { get; set; }
    }
}
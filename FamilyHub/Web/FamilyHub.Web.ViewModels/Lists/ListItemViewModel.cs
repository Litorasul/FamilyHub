namespace FamilyHub.Web.ViewModels.Lists
{
    using System;

    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Services.Mapping;

    public class ListItemViewModel : IMapFrom<ListItem>
    {
        public string Text { get; set; }

        public double? Amount { get; set; }

        public DateTime? DoneDateTime { get; set; }

        public string DoneByUserUserName { get; set; }
    }
}
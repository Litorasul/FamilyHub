namespace FamilyHub.Web.ViewModels.Lists
{
    using System;

    public class ListItemViewModel
    {
        public string Text { get; set; }

        public double? Amount { get; set; }

        public DateTime? DoneDateTime { get; set; }

        public string DoneByUserUserName { get; set; }
    }
}
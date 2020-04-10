namespace FamilyHub.Web.ViewModels.Lists
{
    using System.Collections.Generic;

    public class ListUpdateViewModel
    {
        public int ListId { get; set; }

        public ICollection<ListItemUpdateViewModel> ListItems { get; set; }
    }
}
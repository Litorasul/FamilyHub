namespace FamilyHub.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<EventsDashboardViewModel> Events { get; set; }

        public IEnumerable<ListsDashboardViewModel> Lists { get; set; }

        public IEnumerable<PhotoAlbumsDashboardViewModel> Albums { get; set; }
    }
}

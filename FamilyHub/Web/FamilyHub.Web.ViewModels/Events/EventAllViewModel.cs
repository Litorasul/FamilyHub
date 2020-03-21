namespace FamilyHub.Web.ViewModels.Events
{
    using System.Collections.Generic;

    public class EventAllViewModel
    {
        public IEnumerable<EventSingleViewModel> Events { get; set; }
    }
}
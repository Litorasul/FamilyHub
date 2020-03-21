namespace FamilyHub.Web.ViewModels.Notifications
{
    using System.Collections.Generic;

    public class NotificationAllViewModel
    {
        public IEnumerable<NotificationSingleViewModel> Notifications { get; set; }
    }
}
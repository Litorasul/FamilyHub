namespace FamilyHub.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexEventViewModel> Events { get; set; }
    }
}
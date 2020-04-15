namespace FamilyHub.Web.ViewModels.Administration.Dashboard
{
    using System;

    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Services.Mapping;

    public class ListsDashboardViewModel : IMapFrom<List>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
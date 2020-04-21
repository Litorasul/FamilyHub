namespace FamilyHub.Web.ViewModels.Events
{
    using System;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class EventSingleViewModel : IMapFrom<Event>
    {
        public string Title { get; set; }

        public DateTime Start { get; set; }

        public string CreatorName { get; set; }

        public string Url => $"/Events/{this.Title.Replace(' ', '-')}";
    }
}

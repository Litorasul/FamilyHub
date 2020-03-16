namespace FamilyHub.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class IndexEventViewModel : IMapFrom<Event>
    {
        public IndexEventViewModel()
        {
            this.AssignedUsersUserName = new HashSet<string>();
        }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public string CreatorUserName { get; set; }

        public ICollection<string> AssignedUsersUserName { get; set; }

        public string Url => $"/Events/{this.Title.Replace(' ', '-')}";
    }
}
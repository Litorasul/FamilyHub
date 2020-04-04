namespace FamilyHub.Web.ViewModels.WallPosts
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class WallEventViewModel : IMapFrom<Event>
    {
        public string Title { get; set; }

        public DateTime Start { get; set; }

        public string Description { get; set; }

        public string Url => $"/Events/{this.Title.Replace(' ', '-')}";
    }
}
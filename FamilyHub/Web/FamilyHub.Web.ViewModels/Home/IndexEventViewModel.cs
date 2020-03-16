namespace FamilyHub.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class IndexEventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public IndexEventViewModel()
        {
            this.AssignedUsersUserName = new HashSet<string>();
        }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public string CreatorUserName { get; set; }

        public int AssignedUsersCount { get; set; }

        public ICollection<string> AssignedUsersUserName { get; set; }

        public string Url => $"/Events/{this.Title.Replace(' ', '-')}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Event, IndexEventViewModel>()
                .ForMember(
                    x => x.AssignedUsersUserName,
                    c
                        => c.MapFrom(e => e.AssignedUsers.Select(a => a.User.UserName)));
        }
    }
}

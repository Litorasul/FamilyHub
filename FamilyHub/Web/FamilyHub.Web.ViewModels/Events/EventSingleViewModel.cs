namespace FamilyHub.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class EventSingleViewModel : IMapFrom<Event>, IHaveCustomMappings
    {

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public string CreatorName { get; set; }

        public int AssignedUsersCount { get; set; }

        public ICollection<string> AssignedUsersName { get; set; }

        public string Url => $"/Events/{this.Title.Replace(' ', '-')}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Event, global::FamilyHub.Web.ViewModels.Events.EventSingleViewModel>()
                .ForMember(
                    x => x.AssignedUsersName,
                    c
                        => c.MapFrom(e => e.AssignedUsers.Select(a => a.User.Name)));
        }
    }
}


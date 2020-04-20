namespace FamilyHub.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class EventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public TimeSpan? Duration { get; set; }

        public string CreatorUserName { get; set; }

        public string CreatorId { get; set; }

        public int AssignedUsersCount { get; set; }

        public IEnumerable<string> AssignedUsersName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Event, EventViewModel>()
                .ForMember(
                    x => x.AssignedUsersName,
                    c
                        => c.MapFrom(e => e.AssignedUsers.Select(a => a.User.Name)));
        }
    }
}

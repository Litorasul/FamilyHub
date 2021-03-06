﻿namespace FamilyHub.Web.ViewModels.Administration.Dashboard
{
    using System;

    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class EventsDashboardViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
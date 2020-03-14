﻿// ReSharper disable VirtualMemberCallInConstructor

using FamilyHub.Data.Models.Lists;

namespace FamilyHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Common.Models;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Data.Models.Survey;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.AssignedEvents = new HashSet<UserEvent>();
            this.CreatedSurveys = new HashSet<Survey.Survey>();
            this.Responses = new HashSet<Response>();
            this.AssignedLists = new HashSet<UserList>();
            this.CreatedEvents = new HashSet<Event>();
            this.CreatedLists = new HashSet<List>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public ICollection<UserEvent> AssignedEvents { get; set; }

        public ICollection<Event> CreatedEvents { get; set; }

        public ICollection<Survey.Survey> CreatedSurveys { get; set; }

        public ICollection<Response> Responses { get; set; }

        public ICollection<UserList> AssignedLists { get; set; }

        public ICollection<List> CreatedLists { get; set; }

        public ICollection<ListItem> ListItemsDone { get; set; }
    }
}

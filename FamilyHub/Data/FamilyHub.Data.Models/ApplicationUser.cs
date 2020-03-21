// ReSharper disable VirtualMemberCallInConstructor

namespace FamilyHub.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FamilyHub.Data.Common.Models;
    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Data.Models.Messenger;
    using FamilyHub.Data.Models.PictureAlbums;
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
            this.Conversations = new HashSet<UserConversation>();
            this.Messages = new HashSet<Message>();
            this.PictureAlbums = new HashSet<Album>();
            this.Pictures = new HashSet<UserPicture>();
            this.Notifications = new HashSet<Notification.Notification>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Name { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<UserEvent> AssignedEvents { get; set; }

        public virtual ICollection<Event> CreatedEvents { get; set; }

        public virtual ICollection<Survey.Survey> CreatedSurveys { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

        public virtual ICollection<UserList> AssignedLists { get; set; }

        public virtual ICollection<List> CreatedLists { get; set; }

        public virtual ICollection<ListItem> ListItemsDone { get; set; }

        public virtual ICollection<UserConversation> Conversations { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Album> PictureAlbums { get; set; }

        public virtual ICollection<UserPicture> Pictures { get; set; }

        public virtual ICollection<Notification.Notification> Notifications { get; set; }
    }
}

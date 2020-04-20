namespace FamilyHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.Notification;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Data.Models.WallPosts;
    using FamilyHub.Services.Mapping;

    public class EventsService : IEventsService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;
        private readonly IWallPostsService postsService;
        private readonly IDeletableEntityRepository<Post> postRepository;

        public EventsService(
            IDeletableEntityRepository<Event> eventsRepository,
            IWallPostsService postsService,
            IDeletableEntityRepository<Post> postRepository)
        {
            this.eventsRepository = eventsRepository;
            this.postsService = postsService;
            this.postRepository = postRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Event> query = this.eventsRepository.All().OrderBy(e => e.Start);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var currentEvent = this.eventsRepository
                .All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return currentEvent;
        }

        public async Task UpdateEvent(
            int eventId,
            string title,
            string description,
            DateTime start,
            DateTime end,
            bool isAllDay,
            bool isRecurring,
            string color)
        {
            var eventToUpdate = this.eventsRepository.All().FirstOrDefault(e => e.Id == eventId);

            if (eventToUpdate != null)
            {
                eventToUpdate.Title = title;
                eventToUpdate.Description = description;
                eventToUpdate.Start = start;
                eventToUpdate.End = end;
                eventToUpdate.IsAllDay = isAllDay;
                eventToUpdate.IsRecurring = isRecurring;
                eventToUpdate.Color = color;

                this.eventsRepository.Update(eventToUpdate);
                await this.eventsRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteEvent(int eventId)
        {
            var eventToDelete = this.eventsRepository.All().FirstOrDefault(e => e.Id == eventId);
            var postToDelete = this.postRepository
                .All().FirstOrDefault(p => p.PostType == PostType.NewEvent && p.AssignedEntity == eventId);

            if (eventToDelete != null)
            {
                this.eventsRepository.Delete(eventToDelete);
                await this.eventsRepository.SaveChangesAsync();

                this.postRepository.Delete(postToDelete);
                await this.postRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAllDeleted<T>(int? count = null)
        {
            IQueryable<Event> query = this.eventsRepository.AllWithDeleted()
                .Where(e => e.IsDeleted == true).OrderBy(e => e.Start);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task UnDelete(int eventId)
        {
            var eventTo = this.eventsRepository.AllWithDeleted().FirstOrDefault(e => e.Id == eventId);
            if (eventTo != null && eventTo.IsDeleted == true)
            {
                eventTo.IsDeleted = false;
                eventTo.DeletedOn = null;
                await this.eventsRepository.SaveChangesAsync();
            }
        }

        public T GetByName<T>(string name)
        {
            var currentEvent = this.eventsRepository
                .All().Where(x => x.Title.Replace(" ", "-") == name)
                .To<T>().FirstOrDefault();

            return currentEvent;
        }

        public async Task<int> CreateAsync(
            string title,
            string description,
            DateTime start,
            DateTime end,
            bool isAllDay,
            bool isRecurring,
            string creatorId,
            string color,
            IEnumerable<string> assignedUsersId)
        {
            var eventToAdd = new Event
            {
                Title = title,
                Description = description,
                Start = start,
                End = end,
                IsAllDay = isAllDay,
                IsRecurring = isRecurring,
                CreatorId = creatorId,
                Color = color,
                AssignedUsers = new HashSet<UserEvent>(),
            };

            await this.eventsRepository.AddAsync(eventToAdd);
            await this.eventsRepository.SaveChangesAsync();

            await this.postsService.CreateAsync(creatorId, PostType.NewEvent, eventToAdd.Id, null);

            foreach (var userId in assignedUsersId)
            {
                var userEvent = new UserEvent
                {
                    EventId = eventToAdd.Id,
                    UserId = userId,
                };

                eventToAdd.AssignedUsers.Add(userEvent);
            }

            await this.eventsRepository.SaveChangesAsync();

            return eventToAdd.Id;
        }
    }
}

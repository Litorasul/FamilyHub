﻿using System;
using System.Threading.Tasks;
using FamilyHub.Services.Data.Dtos;

namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;

        public EventService(IDeletableEntityRepository<Event> eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Event> query = this.eventsRepository.All().OrderBy(e => e.StartTime);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var currentEvent = this.eventsRepository
                .All().Where(x => x.Title == name)
                .To<T>().FirstOrDefault();

            return currentEvent;
        }

        public async Task<int> CreateAsync(CreateEventDto dto)
        {
            var eventToAdd = AutoMapperConfig.MapperInstance.Map<Event>(dto);
            //var eventToAdd = new Event
            //{
            //    Title = title,
            //    Description = description,
            //    StartTime = starTime,
            //    EndTime = endTime,
            //    IsFullDayEvent = isFullDayEvent,
            //    IsRecurring = isRecurring,
            //    CreatorId = creatorId,
            //    AssignedUsers = new HashSet<UserEvent>(),
            //};

            await this.eventsRepository.AddAsync(eventToAdd);
            await this.eventsRepository.SaveChangesAsync();

            foreach (var user in eventToAdd.AssignedUsers)
            {
                var userEvent = new UserEvent
                {
                    EventId = eventToAdd.Id,
                    UserId = user.UserId,
                };

                eventToAdd.AssignedUsers.Add(userEvent);
            }

            await this.eventsRepository.SaveChangesAsync();

            return eventToAdd.Id;
        }
    }
}
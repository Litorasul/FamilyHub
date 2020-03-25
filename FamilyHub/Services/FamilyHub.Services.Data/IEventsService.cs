namespace FamilyHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventsService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task<int> CreateAsync(
            string title,
            string description,
            DateTime starTime,
            TimeSpan duration,
            bool isFullDayEvent,
            bool isRecurring,
            string creatorId,
            IEnumerable<string> assignedUsersId);

        T GetById<T>(int id);
    }
}
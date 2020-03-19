using FamilyHub.Services.Data.Dtos;

namespace FamilyHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;

    public interface IEventService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task<int> CreateAsync(CreateEventDto dto);
    }
}
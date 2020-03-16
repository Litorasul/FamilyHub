namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;

    public interface IEventService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
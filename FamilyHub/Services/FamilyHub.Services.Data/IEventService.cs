using System.Runtime.InteropServices.ComTypes;

namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;

    public interface IEventService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
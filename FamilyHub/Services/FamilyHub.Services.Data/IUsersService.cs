namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
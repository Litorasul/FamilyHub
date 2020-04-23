namespace FamilyHub.Services.Data.User
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(string id);
    }
}
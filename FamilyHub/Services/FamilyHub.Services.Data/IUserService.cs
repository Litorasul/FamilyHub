namespace FamilyHub.Services.Data
{
    using System.Collections.Generic;

    public interface IUserService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
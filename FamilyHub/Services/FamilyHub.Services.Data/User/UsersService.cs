﻿namespace FamilyHub.Services.Data.User
{
    using System.Collections.Generic;
    using System.Linq;

    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models;
    using FamilyHub.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query = this.userRepository.All().OrderBy(x => x.UserName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var user = this.userRepository
                .All().Where(u => u.Id == id).To<T>().FirstOrDefault();

            return user;
        }
    }
}

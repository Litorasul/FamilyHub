namespace FamilyHub.Services.Data.Tests.Lists
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data;
    using FamilyHub.Data.Models.Lists;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ListDataModelTests : IDisposable
    {
        private readonly ApplicationDbContext dbContext;

        public ListDataModelTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            this.dbContext = new ApplicationDbContext(options);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        [Fact]
        public void ListShouldHaveTitle()
        {
           var list = new List
           {
               Title = null,
               Type = ListType.ToDoList,
           };

           var validatorResults = new List<ValidationResult>();
           var actual = Validator.TryValidateObject(list, new ValidationContext(list), validatorResults, true);

           Assert.False(actual);
           Assert.Single(validatorResults);
        }

        [Fact]
        public void ListItemShouldHaveText()
        {
            var list = new ListItem()
            {
               Text = null,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(list, new ValidationContext(list), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void UserListShouldHaveUserId()
        {
            var userList = new UserList
            {
                UserId = null,
                ListId = 1,
            };

            Assert.Throws<InvalidOperationException>(() => this.dbContext.UserLists.Add(userList));
        }
    }
}

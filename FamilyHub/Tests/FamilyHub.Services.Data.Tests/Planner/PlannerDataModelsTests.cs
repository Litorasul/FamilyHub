using System.Linq;

namespace FamilyHub.Services.Data.Tests.Planner
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data;
    using FamilyHub.Data.Models.Planner;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PlannerDataModelsTests : IDisposable
    {
        private readonly ApplicationDbContext dbContext;

        public PlannerDataModelsTests()
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
        public void EventShouldHaveTitle()
        {
            var eventToTest = new Event()
            {
                Title = null,
                Start = DateTime.Now,
                CreatorId = "fff",
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(eventToTest, new ValidationContext(eventToTest), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void EventShouldHaveStartCreator()
        {
            var eventToTest = new Event()
            {
                Title = null,
                Start = DateTime.Now,
                CreatorId = "fff",
                End = DateTime.Today,
                Color = "eee",
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(eventToTest, new ValidationContext(eventToTest), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void UserEventShouldHaveUserId()
        {
            var userEvent = new UserEvent
            {
                UserId = null,
                EventId = 1,
            };

            Assert.Throws<InvalidOperationException>(() => this.dbContext.UserEvents.Add(userEvent));
        }
    }
}

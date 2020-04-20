namespace FamilyHub.Services.Data.Tests.Planner
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Models.Planner;
    using Xunit;

    public class PlannerDataModelsTests
    {
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
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(eventToTest, new ValidationContext(eventToTest), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }
    }
}

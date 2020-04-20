namespace FamilyHub.Services.Data.Tests.Planner
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Web.ViewModels.Events;
    using Xunit;

    public class PlannerInputModelTests
    {
        [Fact]
        public void EventCalendarViewModelShouldHaveTitle()
        {
            var eventToTest = new EventCalendarViewModel
            {
                Title = null,
                Start = DateTime.UtcNow,
                Description = "ddd",
                Color = "sss",
                End = DateTime.Now,
                IsAllDay = false,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(eventToTest, new ValidationContext(eventToTest), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void EventCreateInputModelShouldHaveTitle()
        {
            var eventToTest = new EventCreateInputModel
            {
                Title = null,
                Start = DateTime.UtcNow,
                Description = "ddd",
                Color = "sss",
                End = DateTime.Now,
                IsAllDay = false,
                IsRecurring = false,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(eventToTest, new ValidationContext(eventToTest), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void EventUpdateViewModelShouldHaveTitle()
        {
            var eventToTest = new EventUpdateViewModel
            {
                Id = 1,
                Title = null,
                Start = DateTime.UtcNow,
                Description = "ddd",
                Color = "sss",
                End = DateTime.Now,
                IsAllDay = false,
                IsRecurring = false,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(eventToTest, new ValidationContext(eventToTest), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }
    }
}

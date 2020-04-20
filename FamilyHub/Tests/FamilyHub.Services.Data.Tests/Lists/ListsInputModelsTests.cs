namespace FamilyHub.Services.Data.Tests.Lists
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Web.ViewModels.Lists;
    using Xunit;

    public class ListsInputModelsTests
    {
        [Fact]
        public void ListCreateInputModelShouldHaveTitle()
        {
            var list = new ListCreateInputModel()
            {
                Title = null,
                Description = "ddd",
                Type = ListType.ToDoList,
                DueDate = DateTime.MaxValue,
                ListItems = new List<ListItemViewModel>(),
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(list, new ValidationContext(list), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }

        [Fact]
        public void ListItemUpdateViewModelShouldHaveText()
        {
            var list = new ListItemUpdateViewModel
            {
               Id = 1,
               Text = null,
            };

            var validatorResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(list, new ValidationContext(list), validatorResults, true);

            Assert.False(actual);
            Assert.Single(validatorResults);
        }
    }
}
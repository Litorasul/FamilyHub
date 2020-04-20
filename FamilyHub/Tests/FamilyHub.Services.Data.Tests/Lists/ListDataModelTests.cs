namespace FamilyHub.Services.Data.Tests.Lists
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data;
    using FamilyHub.Data.Models.Lists;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ListDataModelTests
    {
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
    }
}

namespace FamilyHub.Web.ViewModels.Lists
{
    using System.ComponentModel.DataAnnotations;

    using FamilyHub.Data.Models.Lists;
    using FamilyHub.Services.Mapping;

    public class ListItemUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
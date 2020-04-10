namespace FamilyHub.Web.ViewModels.Lists
{
    using System.ComponentModel.DataAnnotations;

    public class ListItemUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
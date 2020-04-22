namespace FamilyHub.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class WeatherInputModel
    {
        [Required]
        public double Lat { get; set; }

        [Required]
        public double Lon { get; set; }
    }
}
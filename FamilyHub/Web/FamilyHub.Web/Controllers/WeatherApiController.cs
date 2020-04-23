namespace FamilyHub.Web.Controllers
{
    using FamilyHub.Services.Data;
    using FamilyHub.Services.Data.Weather;
    using FamilyHub.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/[controller]")]
    public class WeatherApiController : ControllerBase
    {
        private readonly IWeatherService weatherService;

        public WeatherApiController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post(WeatherInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var data = this.weatherService.GetCurrentWeather(model.Lat, model.Lon);

            return this.Ok(data);
        }
    }
}

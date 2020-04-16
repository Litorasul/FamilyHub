﻿using FamilyHub.Web.ViewModels;

namespace FamilyHub.Web.Controllers
{
    using FamilyHub.Services.Data;
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
            var data = this.weatherService.GetCurrentWeather(model.Lat, model.Lon);

            return this.Ok(data);
        }
    }
}

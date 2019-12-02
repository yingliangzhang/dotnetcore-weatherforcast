using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecastStation.Model;
using WeatherForecastStation.Dto;
using AutoMapper;
using WeatherForecastStation.Service;

namespace WeatherForecastStation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IMapper mapper,
            IWeatherForecastService weatherForecastService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public List<WeatherForecastDto> GetAll()
        {
            _logger.LogInformation("Fetching all weather forecasts...");
            var weatherforcasts = _weatherForecastService.GetAllWeatherForecasts();
            var result = _mapper.Map<List<WeatherForecast>, List<WeatherForecastDto>>(weatherforcasts);
            _logger.LogInformation("Fetched all weather forecasts. Found {Count}", result.Count());
            return result;
        }
    }
}

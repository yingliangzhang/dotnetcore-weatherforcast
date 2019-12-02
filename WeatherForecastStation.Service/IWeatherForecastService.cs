using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecastStation.Model;

namespace WeatherForecastStation.Service
{
    public interface IWeatherForecastService
    {
        List<WeatherForecast> GetAllWeatherForecasts();
    }
}

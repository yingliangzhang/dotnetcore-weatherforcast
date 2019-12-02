using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastStation.Model;
using WeatherForecastStation.Repository;

namespace WeatherForecastStation.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IRepository<WeatherForecast> _repository;
        public WeatherForecastService(IRepository<WeatherForecast> repository)
        {
            _repository = repository;
        }
        public List<WeatherForecast> GetAllWeatherForecasts()
        {
            return _repository.GetAll();
        }
    }
}

using AutoMapper;
using WeatherForecastStation.Model;
using WeatherForecastStation.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecastStation.Mapper
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<WeatherForecast, WeatherForecastDto>();
        }
    }
}

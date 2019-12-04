using System;
using Xunit;
using Moq;
using WeatherForecastStation.Repository;
using WeatherForecastStation.Model;
using System.Collections.Generic;

namespace WeatherForecastStation.Service.Test
{
    public class WeatherForecastServiceTests
    {
        private readonly Mock<IRepository<WeatherForecast>> _repositoryMock;
        private readonly WeatherForecastService _itemToTest;

        public WeatherForecastServiceTests()
        {
            _repositoryMock = new Mock<IRepository<WeatherForecast>>();
            _itemToTest = new WeatherForecastService(_repositoryMock.Object);
        }

        [Fact]
        public void WeatherForecastService_Returns_List_Of_WeatherForecasts_When_Repository_Returns_A_Collection()
        {
            var weatherForecasts = new List<WeatherForecast>()
            {
                new WeatherForecast
                {
                    Summary = "test summary 1",
                    Date = DateTime.UtcNow,
                    TemperatureC = 20,
                },
                new WeatherForecast
                {
                    Summary = "test summary 2",
                    Date = DateTime.UtcNow,
                    TemperatureC = 22,
                }
            };

            _repositoryMock.Setup(r => r.GetAll()).Returns(weatherForecasts);

            var results = _itemToTest.GetAllWeatherForecasts();

            Assert.Equal(2, results.Count);
            results.ForEach(item => Assert.IsType<WeatherForecast>(item));
            Assert.Contains(results, item => item.Summary == "test summary 1");
            Assert.Contains(results, item => item.Summary == "test summary 2");
        }
    }
}

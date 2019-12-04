using System;
using Xunit;
using Moq;
using WeatherForecastStation.Service;
using WeatherForecastStation.Model;
using WeatherForecastStation.Dto;
using Microsoft.Extensions.Logging;
using WeatherForecastStation.Controllers;
using AutoMapper;
using WeatherForecastStation.Mapper;
using System.Collections.Generic;

namespace WeatherForecastStation.Web.Test
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;
        private readonly IMapper _mapper;
        private readonly Mock<IWeatherForecastService> _weatherForecastService;
        private readonly WeatherForecastController itemUnderTest;

        public WeatherForecastControllerTests()
        {
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
            _mapper = new MapperConfiguration(config => config.AddProfile(new WeatherForecastProfile())).CreateMapper();
            _weatherForecastService = new Mock<IWeatherForecastService>();
            itemUnderTest = new WeatherForecastController(_loggerMock.Object, _mapper, _weatherForecastService.Object);
        }

        [Fact]
        public void GetAllAsync_GivenWeatherForecastsExist_Returns_CollectionOfWeahterforecasts()
        {
            var expected = new List<WeatherForecast>()
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

            _weatherForecastService
                .Setup(service => service.GetAllWeatherForecasts())
                .Returns(expected);

            var result = itemUnderTest.GetAll();
            var collection = Assert.IsType<List<WeatherForecastDto>>(result);

            Assert.Equal(2, collection.Count);
            collection.ForEach(item => Assert.IsType<WeatherForecastDto>(item));
            Assert.Contains(collection, item => item.Summary == "test summary 1");
            Assert.Contains(collection, item => item.Summary == "test summary 2");

            _loggerMock.Verify(
                m => m.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Fetching all weather forecasts...", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);

            _loggerMock.Verify(
                m => m.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Fetched all weather forecasts. Found 2", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}

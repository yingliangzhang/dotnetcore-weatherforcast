using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastStation.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
    }
}

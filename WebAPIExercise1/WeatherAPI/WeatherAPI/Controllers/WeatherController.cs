using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    [RoutePrefix("weather")]
    public class WeatherController : ApiController
    {
        // static in memory list of weatherinfo
        private static List<WeatherInformation> weatherList = new List<WeatherInformation>()
        {
            new WeatherInformation { City = "Dublin", Condition = "Snowing", Temperature = -3.0, Warning = true, WindSpeed = 25 },
            new WeatherInformation { City = "Stuttgart", Condition = "Raining", Temperature = 12.5, Warning = false, WindSpeed = 12 },
            new WeatherInformation { City = "Barcelona", Condition = "Sunny", Temperature = 21.3, Warning = false, WindSpeed = 15 },
            new WeatherInformation { City = "Cork", Condition = "Windy", Temperature = 3.2, Warning = true, WindSpeed = 18 }
        };

        //GET: weather/all
        [Route("all")]
        public IHttpActionResult GetAllCities()
        {
            return Ok(weatherList.OrderByDescending(c => c.City).ToList());
        }

        // GET: weather/city/{name}
        [Route("city/{name}")]
        [HttpGet]
        public IHttpActionResult GetCity(string name)
        {
            WeatherInformation weather = weatherList.SingleOrDefault(w => w.City.ToUpper() == name.ToUpper());
            if (weather == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(weather);
            }
        }

        // POST: weather/all
        [Route("all/{warning:bool}")]
        [HttpGet]
        public IHttpActionResult GetWeatherCities(bool warning)
        {
            if (ModelState.IsValid)
            {
                var cities = weatherList.Where(w => w.Warning == warning).ToList();
                if(cities != null)
                {
                    return Ok(cities);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
        // PUT: weather/city/{city}
        [Route("city/{city}")]
        [HttpPut]
        public IHttpActionResult UpdateWeatherInformation(string city, WeatherInformation weather)
        {
            if (ModelState.IsValid)
            {
                var existing = weatherList.SingleOrDefault(w => w.City.ToUpper() == city.ToUpper());
                if(existing == null)
                {
                    return NotFound();
                }
                else
                {
                    existing.Condition = weather.Condition;
                    existing.Temperature = weather.Temperature;
                    existing.Warning = weather.Warning;
                    existing.WindSpeed = weather.WindSpeed;
                    return Ok(existing);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }       
    }
}

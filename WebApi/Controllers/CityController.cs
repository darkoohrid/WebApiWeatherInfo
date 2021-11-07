using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DB;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        readonly AppDbContext _database;

        public CityController(AppDbContext database)
        {
            _database = database;
        }

        [Route("/ListCities")]
        [HttpGet]
        public ActionResult ListCities()
        {
            IList<City> cities = _database.Cities.Include(c => c.Country).ToList();
            return Ok(cities);        
        }

        [Route("/CreateCity")]
        [HttpPost]
        public ActionResult CreateCity(string cityName, string countryName)
        {
            bool cityExist = _database.Cities.Any(c => c.Name == cityName);
            bool countryExist = _database.Countries.Any(c => c.Name == countryName);
            if (string.IsNullOrWhiteSpace(cityName) || cityExist || !countryExist) return BadRequest();

            var selectedCountryId = _database.Countries.FirstOrDefault(c => c.Name == countryName).Id;
            var createdCity = new City { CountryId = selectedCountryId, Name = cityName};
            _database.Cities.Add(createdCity);
            _database.SaveChanges();

            return Ok(createdCity);
        }
    }
}

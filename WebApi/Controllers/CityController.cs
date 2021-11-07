using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DB;
using WebApi.Models;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        // Accessing the data from database
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
            // Checking if City already exist or Country doesn't exist, also validation for NullOrWhiteSpace
            bool cityExist = _database.Cities.Any(c => c.Name == cityName);
            bool countryExist = _database.Countries.Any(c => c.Name == countryName);
            if (string.IsNullOrWhiteSpace(cityName) || cityExist || !countryExist) return BadRequest();

            // Getting the CountryId from Existing County
            var selectedCountryId = _database.Countries.FirstOrDefault(c => c.Name == countryName).Id;
            // Saving newly created City to be stored in database and returned.
            var createdCity = new City { CountryId = selectedCountryId, Name = cityName};
            _database.Cities.Add(createdCity);
            _database.SaveChanges();

            return Ok(createdCity);
        }
    }
}

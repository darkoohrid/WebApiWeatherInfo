using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CountryController : ControllerBase
    {
        // Accessing the data from database
        readonly AppDbContext _database;

        public CountryController(AppDbContext database)
        {
            _database = database;
        }

        [Route("/CreateCountry")]
        [HttpPost]
        public ActionResult CreateCountry(string inpCountry)
        {
            // Checking if Country already exist , also checking for NullOrWhiteSpace
            bool countryExist = _database.Countries.Any(c => c.Name == inpCountry);
                if (!countryExist && !string.IsNullOrWhiteSpace(inpCountry)) { 
                // If conditions are satisfied add to database and save changes
                    _database.Countries.Add(new Country { Name = inpCountry });
                    _database.SaveChanges();
                }
            // getting the country from database to show the automaticaly created ID [Key] aswell
            var createdCountry = _database.Countries.First(t => t.Name == inpCountry);
            return Ok(createdCountry);
        }

        [Route("/CreateCountries")]
        [HttpPost]
        public ActionResult CreateCountries(List <string> inputCountries)
        {
            List<Country> successfullyCreatedCountries = new List<Country>();
            foreach(var inpCountry in inputCountries)
            {
                bool countryExist = _database.Countries.Any(c => c.Name == inpCountry);
                if (!countryExist && !string.IsNullOrWhiteSpace(inpCountry))
                {
                    _database.Countries.Add(new Country { Name = inpCountry });
                    _database.SaveChanges();
                    successfullyCreatedCountries.Add(_database.Countries.First(t=>t.Name == inpCountry));
                }
            }
            return Ok(successfullyCreatedCountries);
        }
    }
}
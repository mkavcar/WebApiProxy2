using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTestCore1.Interfaces;
using WebApiTestCore1.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiTestCore1.Controllers
{
    [Route("api/[controller]")]
    public class CountryController : Controller
    {
        private ICountryService countryService;
        private ILogger<CountryController> logger;

        public CountryController(ILogger<CountryController> Logger, ICountryService CountryService)
        {
            logger = Logger;
            countryService = CountryService;
        }

        // GET: api/country
        [HttpGet]
        public IEnumerable<Country> GetAll()
        {
            logger.LogInformation("Get Country List");
            return countryService.GetAll();
        }

        // GET api/country/us
        [HttpGet("{code}", Name = "GetCountry")]
        public IActionResult GetByCode(string code)
        {
            logger.LogInformation("Get Country: {code}", code);
            var country = countryService.Get(code);

            if (country != null)
                return new ObjectResult(country);
            else
                return NotFound();
        }

        // POST api/country
        [HttpPost]
        public void Post([FromBody]Country country)
        {
            logger.LogInformation("Add Country: {country.Code}", country.Code);
            countryService.Add(country);
        }

        // PUT api/country
        [HttpPut]
        public void Put([FromBody]Country country)
        {
            logger.LogInformation("Update Country: {country.Code}", country.Code);
            countryService.Update(country);
        }

        // DELETE api/country/us
        [HttpDelete("{code}")]
        public void Delete(string code)
        {
            logger.LogInformation("Delete Country: {code}", code);
            countryService.Delete(code);
        }
    }
}

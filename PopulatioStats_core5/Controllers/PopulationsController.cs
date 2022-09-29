using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopulatioStats_core5.Models;

namespace PopulatioStats_core5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulationsController : ControllerBase
    {
        private readonly PopuContext _context;

        public PopulationsController(PopuContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Population>>> GetPopulations()
        {
            return await _context.Populations.ToListAsync();
        }


        [HttpGet("{id}")]
        //public async Task<IQueryable<Country>> GetCountry(string name)
        public async Task<ActionResult<Population>> GetCountrybyPopId(int id)
        {
            var population = await _context.Populations.FindAsync(id);
            return population != null ? population : NotFound();
        }


        [HttpPost]
        public async Task<ActionResult<Population>> PostPopulation(Population population)
        {

            if (population == null) return BadRequest();
            try
            {
                _context.Add(population);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Population>> UpdatePopulationId(Population population, int id)
        {

            if (population == null || population.PopulationId != id) return BadRequest();
            _context.Entry(population).State = EntityState.Modified;

            try
            {
                population.PopulationId = id; // updating id
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                if (!_context.Populations.Any(pop => pop.PopulationId == id)) return NotFound();
            }

            return NoContent();

        }

        //api/person/byName?firstName=a&lastName=b
        //[HttpGet("byName")]
        //public string Get(string firstName, string lastName, string address)
        //{
        //}

        // https://stackoverflow.com/questions/36280947/how-to-pass-multiple-parameters-to-a-get-method-in-asp-net-core

        //https://stackoverflow.com/questions/21140305/web-api-routing-with-multiple-parameters


        [HttpPut("Countries/{countryId}")]
        public async Task<ActionResult<Population>> UpdateCountryId(Population population, int countryId)
        {

            if (population == null || population.CountryId != countryId) return BadRequest();
            _context.Entry(population).State = EntityState.Modified;

            try
            {
                population.CountryId = countryId; // updating id
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                if (!_context.Populations.Any(pop => pop.CountryId == countryId)) return NotFound();
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Population>> DeletePopulation(int id)
        {
            var population = await _context.Populations.FindAsync(id);
            if (population == null) return NotFound();

            _context.Populations.Remove(population);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

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
    public class YearValsController : ControllerBase
    {
        private readonly PopuContext _context;

        public YearValsController(PopuContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<YearVal>>> GetYearVal()
        {
            return await _context.YearVals.ToListAsync();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<YearVal>> GetYearVal(int id)
        {
            var year = await _context.YearVals.FindAsync(id);
            return year != null ? year : NotFound();
        }



        [HttpPost]
        public async Task<ActionResult<YearVal>> PostYearVal(YearVal yearVal)
        {

            if (yearVal == null) return BadRequest();
            try
            {
                _context.Add(yearVal);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<YearVal>> UpdateYearValId(YearVal yearVal, int id)
        {

            if (yearVal == null || yearVal.YearValId != id) return BadRequest();
            _context.Entry(yearVal).State = EntityState.Modified;

            try
            {
                yearVal.YearValId = id; // updating id
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                if (!_context.YearVals.Any(pop => pop.YearValId == id)) return NotFound();
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Population>> DeleteYearVal(int id)
        {
            var yearVal = await _context.YearVals.FindAsync(id);
            if (yearVal == null) return NotFound();

            _context.YearVals.Remove(yearVal);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}

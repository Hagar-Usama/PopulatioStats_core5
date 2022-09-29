using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopulatioStats_core5.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PopulatioStats_core5.Controllers;

namespace PopulatioStats_core5.Controllers
{

    public class PopulationCount
    {
        public int year { get; set; }
        public ulong value { get; set; }

        public PopulationCount(int year, ulong value)
        {
            this.year = year;
            this.value = value;
        }
    }
    public class ReqResponse
    {
        public string country { get; set; }

        public object s { get; set; }
        public PopulationCount populationCount { get; set; }

        public ReqResponse()
        {

        }
        public ReqResponse(string country, PopulationCount populationCount)
        {
            this.country = country;
            this.populationCount = populationCount;
        }
    }


    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly PopuContext _context;

        public CountriesController(PopuContext context)
        {
            _context = context;
        }


        //[HttpGet]
        //public List<Country> Get() => _context.Countries.ToList();

        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Country>> GetCountry(int id) {

        //        var country = await _context.Countries.FindAsync(id);
        //        return (country == null) ? NotFound() : country;

        //}

        [HttpGet("{name}")]
        //public async Task<IQueryable<Country>> GetCountry(string name)
        public async Task<Country> GetCountry(string name)
        {
            var res = await _context.Countries.FirstOrDefaultAsync(i => String.Equals(i.Name, name));

            return res;

        }

        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetCountry()
        {
            List<Country> nestedList = new List<Country>();
            var countryList = _context.Countries.ToList();

            // return list of years that has the corresponding to pop id

            //foreach( var country in countryList)
            //{
            //    // get pops where id; matches
            //    var populations = _context.Populations.Where(x => x.CountryId == country.CountryId).ToList();
            //    //get years for each pop
            //    foreach (var popu in populations)
            //    {
            //        var years = _context.YearVals.Where(x => x.YearValId == popu.YearValId).ToList();
            //    }
            //}

            // new {Country Country}

            //var result = from country in _context.Countries
            //             join population in _context.Populations on country.CountryId equals population.CountryId
            //             join yearVal in _context.YearVals on population.YearValId equals yearVal.YearValId
            //             //where c.Type == "HTTP Status"
            //             select new
            //             {
            //                 country.Name,
            //                 yearVal.Year,
            //                 yearVal.Value
            //             };

            //var query =
            //           (from country in _context.Countries
            //            join population in _context.Populations on country.CountryId equals population.CountryId
            //            join yearVal in _context.YearVals on population.YearValId equals yearVal.YearValId
            //            //where country.CountryId == id
            //            select new { country = country.Name, populationCounts = new { year = yearVal.Year, value = yearVal.Value } }).ToList();


            var query1 =
                       (from country in _context.Countries
                        join population in _context.Populations on country.CountryId equals population.CountryId
                        join yearVal in _context.YearVals on population.YearValId equals yearVal.YearValId
                        //where country.CountryId == id
                        //select new { country = country.Name, populationCounts = new { year = yearVal.Year, value = yearVal.Value } }).ToList();
                        select new ReqResponse(country.Name, new PopulationCount(yearVal.Year, yearVal.Value))).ToList();

            var GroupByCountryName = query1.GroupBy(s => s.country).Select(res => new
            {
                country = res.Key,
                PopulationCounts= res.Select(a=>a.populationCount).ToList()
            }).ToList();

            return GroupByCountryName;


            IEnumerable<IGrouping<string, ReqResponse>> GroupByQS = (from rq in query1
                                                                     group rq by rq.country);


            // https://docs.microsoft.com/en-us/dotnet/csharp/linq/group-query-results
            //var groupByLastNamesQuery =
            //from student in students
            //group student by student.LastName into newGroup
            //orderby newGroup.Key
            //select newGroup;


            //************************************************************************

            var query =
                           (from country in _context.Countries
                            select new
                            {
                                country = country.Name,
                                populationCounts = (from population in _context.Populations
                                                    join yearVal in _context.YearVals on population.YearValId equals yearVal.YearValId
                                                    where population.CountryId == country.CountryId
                                                    select new { year = yearVal.Year, value = yearVal.Value }).ToList()
                            }).ToList();




            //var innerQuery = (from population in _context.Populations
            //                 join yearVal in _context.YearVals on population.YearValId equals yearVal.YearValId
            //                 where population.CountryId = country.CountryId
            //                 select new { year = yearVal.Year, value = yearVal.Value }).ToList();
            return query;

            // parsing manually
            
            
            //foreach (var country in countryList)
            //{
            //return query;
                
            //    var nestedCountry = new Country
            //    {
            //        CountryId = country.CountryId,
            //        Name = country.Name,
            //        Populations = _context.Populations.Where(x => x.CountryId == country.CountryId).ToList()
            //    };

            //    foreach (var pop in nestedCountry.Populations)
            //    {
            //        pop.YearVals = _context.YearVals.Where(x => x.PopulationId == pop.PopulationId).FirstOrDefault();
            //    }

            //    nestedList.Add(nestedCountry);
            //}

            
            //var output = JsonConvert.DeserializeObject<List<List>(nestedList);

            return nestedList;
        }

        [HttpPost]
        //public async Task<IQueryable<Country>> GetCountry(string name)
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {

            if (country == null) return BadRequest();
            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();

        }

        [HttpPut("{id}")]
        //public async Task<IQueryable<Country>> GetCountry(string name)
        public async Task<ActionResult<Country>> PutCountry(Country country, int id)
        {

            if (country == null || country.CountryId != id) return BadRequest();
            
            _context.Entry(country).State = EntityState.Modified;

            try
            {
                country.CountryId = id; // updating id
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                if (!_context.Countries.Any(co => co.CountryId == id))  return NotFound();
                

               
            }

            /* Microsoft Docs */
            // According to the HTTP specification, a PUT request requires the client
            // to send the entire updated entity, not just the changes.
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null) return NotFound();

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}

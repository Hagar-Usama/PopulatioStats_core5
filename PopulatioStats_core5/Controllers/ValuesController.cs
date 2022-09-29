using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using PopulatioStats_core5.Models;

namespace PopulatioStats_core5.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : Controller
    {

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {

            // importing external data
            var client = new RestClient($"https://countriesnow.space/api/v0.1/countries/population");
            var request = new RestRequest();
            RestResponse response = await client.ExecuteAsync(request);
            
            dynamic responseContent = null;
            while (responseContent == null)
            {
                responseContent = JsonConvert.DeserializeObject(response.Content);
            }

            string path = @"C:\Users\hagarusama\source\repos\population.json";
            // string jsonText = File.ReadAllText(path);

            StreamReader r = new StreamReader(path);
            string jsonString = r.ReadToEnd();

            var outputData = JsonConvert.DeserializeObject<JsonData>(jsonString);

            // Todo: parse content
            // Todo: update db accordingly
            // Todo: move from here

            //Console.WriteLine(responseContent);
            //if (responseContent != null)
            //{
            //    foreach (dynamic i in responseContent)
            //    {
            //        string country = i.country;

            //        foreach (dynamic j in i)
            //        {

            //        }
            //    }


            //}

            return new string[] { "value 1", "value 2", "value 3", "value 4" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "The value is " + id;
        }

        // what is this for?
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}

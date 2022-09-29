using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopulatioStats_core5.Models
{
    public class DataItem
    {
        public string country { get; set; }
        public string code { get; set; }
        public string iso3 { get; set; }

        //list of population items
        public PopItem[] populationCounts { get; set; }
    }
}

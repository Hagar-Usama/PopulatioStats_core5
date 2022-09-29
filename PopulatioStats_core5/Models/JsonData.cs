using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopulatioStats_core5.Models
{
    public class JsonData
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public DataItem[] data { get; set; }
    }
}

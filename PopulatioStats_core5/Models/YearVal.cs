using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PopulatioStats_core5.Models
{
    public class YearVal
    {
        
        [Key]
        [Required]
        public int YearValId { get; set; }
        //[ForeignKey("Population")]
        //[Required]
        [Required]
        public int Year { get; set; }
        [Required]
        public ulong Value { get; set; }

        //Navigation Property
        public int PopulationId { get; set; }
        public List<Population> Populations { get; set; }
    }
}

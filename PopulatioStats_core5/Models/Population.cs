using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PopulatioStats_core5.Models
{
    public class Population
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PopulationId { get; set; }
        //[ForeignKey("Country")]
        
        //[ForeignKey("YearVal")]
        

        //Navigation Prop
        public int CountryId { get; set; }
        public Country country { get; set; }
        public int YearValId { get; set; }
        public YearVal YearVals  { get; set; }
        
        //[ForeignKey("YearVal")]
        //public int YearId { get; set; }
    }
}

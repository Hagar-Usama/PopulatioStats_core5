using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace PopulatioStats_core5.Models
{
    public class PopuContext : DbContext
    {
        public PopuContext(DbContextOptions<PopuContext> options)
           : base(options)
        {

        }
    //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
       
    //}
        public DbSet<Country> Countries { get; set; }
        public DbSet<Population> Populations { get; set; }
        public DbSet<YearVal> YearVals { get; set; }
    }
}

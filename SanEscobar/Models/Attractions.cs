using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SanEscobar.Models
{
    public class Attraction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Added {get ; set ;}
    }

    public class AttrDBCtxt : DbContext
    {
        public DbSet<Attraction> Attractions { get; set; } 
    }
}
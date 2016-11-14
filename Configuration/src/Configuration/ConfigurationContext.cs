using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
 

namespace Configuration
{
    public class ConfigurationContext:DbContext
    {
        public ConfigurationContext(DbContextOptions options):base(options)
        {
       
        }
        public DbSet<ConfigurationValue> Values { get; set; }


    }


}

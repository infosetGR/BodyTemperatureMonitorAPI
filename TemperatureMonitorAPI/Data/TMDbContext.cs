using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMonitorAPI.Models;

using Microsoft.EntityFrameworkCore;


namespace TemperatureMonitorAPI.Data
{
     public class TMContext : DbContext
    {
        public TMContext(DbContextOptions<TMContext> opt) : base(opt) { }

        public DbSet<TemperatureLogEntry> TemperatureLogEntries { get; set; }
        public DbSet<PatientDetail> PatientDetails { get; set; }

        public DbSet<User> Users { get; set; }
    }


}

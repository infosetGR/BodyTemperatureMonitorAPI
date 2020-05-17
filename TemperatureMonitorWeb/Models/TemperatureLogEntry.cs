using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureMonitorWeb.Models
{
    public class TemperatureLogEntry
    {
        public int Id { get; set; }

        [Required]
        public float BodyTemperatureC { get; set; }
        [Required]

        public float BodyTemperatureF => 32 + (int)(BodyTemperatureC / 0.5556);

        public User User { get; set; }
        public DateTime Created { get; set; }

    }

   
}

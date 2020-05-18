using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureMonitorWeb.Models
{
    public class Temp
    {
        public int Id { get; set; }

        [Required]
        [Range(35, 42, ErrorMessage = "Enter number between 35 and 42 degrees Celcious")]
        public float BodyTemperatureC { get; set; }

        public float BodyTemperatureF => 32 + (int)(BodyTemperatureC / 0.5556);
        [Required]
        public int UserId { get; set; }
        public PatientDetail PatientDetail { get; set; }
        public DateTime Created { get; set; }

    }

   
}

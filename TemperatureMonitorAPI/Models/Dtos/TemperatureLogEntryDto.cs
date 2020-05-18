using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureMonitorAPI.Models.Dtos
{
    public class TemperatureLogEntryDto
    {
        public int Id { get; set; }

        [Required]
        public float BodyTemperatureC { get; set; }
        [Required]

        public float BodyTemperatureF => 32 + (int)(BodyTemperatureC / 0.5556);

        [Required]        
        public int UserId { get; set; }
     
        public DateTime Created { get; set; }

    }

   
}

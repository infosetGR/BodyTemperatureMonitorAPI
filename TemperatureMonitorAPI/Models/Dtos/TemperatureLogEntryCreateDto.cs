using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureMonitorAPI.Models.Dtos
{
    public class TemperatureLogEntryCreateDto
    {
        
        [Required]
        public float BodyTemperatureC { get; set; }
    
    }

   
}

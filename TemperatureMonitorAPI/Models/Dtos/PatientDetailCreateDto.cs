using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TemperatureMonitorAPI.Models.Dtos
{
    public class PatientDetailCreateDto
    {
        
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }

        public byte[] Picture { get; set; }


    }
}

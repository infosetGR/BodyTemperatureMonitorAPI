using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureMonitorAPI.Models
{
    public class TemperatureLogEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public float BodyTemperatureC { get; set; }

        public float BodyTemperatureF => 32 + (int)(BodyTemperatureC / 0.5556);

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public PatientDetail PatientDetail { get; set; }
        public DateTime Created { get; set; }

    }

   
}

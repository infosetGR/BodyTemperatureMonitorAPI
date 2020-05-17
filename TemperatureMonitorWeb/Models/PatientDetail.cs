using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureMonitorWeb.Models
{
    public class PatientDetail
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public byte[] Picture { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }


    }
}

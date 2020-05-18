using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureMonitorWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<PatientDetail> PatientList { get; set; }
        public IEnumerable<Temp> TempList { get; set; }
    }
}

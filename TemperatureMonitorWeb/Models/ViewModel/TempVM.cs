using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TemperatureMonitorWeb.Models.ViewModel
{
    public class TempVM
    {
        public IEnumerable<SelectListItem> PatientList { get; set; }

        public Temp Temp { get; set; }

    }
}

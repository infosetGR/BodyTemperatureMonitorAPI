using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureMonitorWeb
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44396/";
        public static string PatientDetailsAPIPath = APIBaseUrl+"api/v1/PatientDetails/";
        public static string TemperaturesLogEntryAPIPath = APIBaseUrl+"api/v1/TemperaturesLogEntry/";
    }
}

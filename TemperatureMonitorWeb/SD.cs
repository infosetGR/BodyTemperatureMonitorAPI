using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemperatureMonitorWeb
{
    public static class SD
    {
        public static string APIBaseUrl = "http://TemperatureMonitorAPI/"; // when in docker
       // public static string APIBaseUrl = "https://localhost:44396/";  //for vs dev
        public static string PatientDetailsAPIPath = APIBaseUrl+"api/v1/PatientDetails/";
        public static string TemperaturesLogEntryAPIPath = APIBaseUrl+"api/v1/TemperatureLogEntry/";
        public static string AccountAPIPath = APIBaseUrl + "api/v1/Users/";
    }
}

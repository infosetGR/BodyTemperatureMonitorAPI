using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMonitorAPI.Models;

namespace TemperatureMonitorAPI.Repository.IRepository
{
    public interface ITemperatureLogEntryRepository
    {
        TemperatureLogEntry GetTemperatureLogEntry(int id);
        ICollection<TemperatureLogEntry> GetTemperatureLogEntriesForPatient(int Userid);
        ICollection<TemperatureLogEntry> GetFeverLogEntriesForPatientAndPeriod(int Userid, DateTime start, DateTime end);
        bool CreateTemperatureLogEntry(TemperatureLogEntry t);
        bool DeleteTemperatureLogEntry(TemperatureLogEntry t);
        bool Exists(int id);
        bool Save();
    }
}

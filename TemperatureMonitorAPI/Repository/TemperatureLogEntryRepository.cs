using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMonitorAPI.Models;
using TemperatureMonitorAPI.Data;
using TemperatureMonitorAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace TemperatureMonitorAPI.Repository
{
    public class TemperatureLogEntryRepository :ITemperatureLogEntryRepository
    {
        private readonly TMContext _db;
        public TemperatureLogEntryRepository(TMContext db)
        {
            _db = db;
        }

        public TemperatureLogEntry GetTemperatureLogEntry(int id)
        {
                return _db.TemperatureLogEntries.FirstOrDefault(a => a.Id == id);
        }

        public bool DeleteTemperatureLogEntry(TemperatureLogEntry t)
        {
            _db.TemperatureLogEntries.Remove(t);
            return Save();
        }

        public ICollection<TemperatureLogEntry> GetFeverLogEntriesForPatientAndPeriod(int Userid, DateTime start, DateTime end)
        {
            return _db.TemperatureLogEntries.Include(c => c.PatientDetail).Where(c => c.PatientDetail.UserId == Userid).Where(c => c.BodyTemperatureC>37).ToList<TemperatureLogEntry>();
        }

        public ICollection<TemperatureLogEntry> GetTemperatureLogEntriesForPatient(int? Userid)
        {
            return _db.TemperatureLogEntries.Include(c => c.PatientDetail).Where(c => Userid==null || c.PatientDetail.UserId == Userid.GetValueOrDefault() ).ToList<TemperatureLogEntry>();
        }
        public bool Exists(int id)
        {
            return _db.TemperatureLogEntries.Any(a => a.Id == id);
        }


        public bool CreateTemperatureLogEntry(TemperatureLogEntry t)
        {
            t.Created = DateTime.Now;
            _db.TemperatureLogEntries.Add(t);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

       
    }
}

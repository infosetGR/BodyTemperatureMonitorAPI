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
    public class PatientDetailRepository : IPatientDetailRepository
    {
        private readonly TMContext _db;

        public PatientDetailRepository(TMContext db)
        {
            _db = db;
        }

        public bool CreatePatientDetail(PatientDetail p)
        {
            p.Created = DateTime.Now;
            _db.Add(p);
            
            return Save();
        }

        public bool DeletePatientDetail(PatientDetail p)
        {
            _db.Remove(p);
            return Save();
        }

        public bool Exists(string email)
        {
            return _db.PatientDetails.Any(a => a.Email == email);
        }

        public bool Exists(int Userid)
        {
            return _db.PatientDetails.Any(a => a.UserId == Userid);
        }


        public PatientDetail GetPatientDetail(int Userid)
        {
            return _db.PatientDetails.Include(c => c.User).Where(c => c.UserId == Userid).FirstOrDefault();
        }

        public ICollection<PatientDetail> GetAllPatientDetail()
        {
            return _db.PatientDetails.ToList<PatientDetail>();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePatientDetail(PatientDetail p)
        {
            p.Modified = DateTime.Now;
            _db.PatientDetails.Update(p);
            return Save();
        }
    }
}

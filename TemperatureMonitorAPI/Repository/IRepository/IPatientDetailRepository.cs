using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemperatureMonitorAPI.Models;

namespace TemperatureMonitorAPI.Repository.IRepository
{
    public interface IPatientDetailRepository
    {
        PatientDetail GetPatientDetail(int Userid);
        bool CreatePatientDetail(PatientDetail p);
        bool UpdatePatientDetail(PatientDetail p);
        bool DeletePatientDetail(PatientDetail p);
        ICollection<PatientDetail> GetAllPatientDetail();
        bool Exists(string Email);
        bool Exists(int Userid);

        bool Save();

    }
}

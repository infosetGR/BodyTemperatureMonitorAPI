using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TemperatureMonitorWeb.Models;
using TemperatureMonitorWeb.Repository.IRepository;

namespace TemperatureMonitorWeb.Repository
{
    public class PatientDetailRepository : Repository<PatientDetail>, IPatientDetailRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public PatientDetailRepository(IHttpClientFactory clientFactory):base(clientFactory)
        {
            _clientFactory = clientFactory;
        }


    }
}

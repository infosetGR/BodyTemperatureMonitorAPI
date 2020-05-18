using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TemperatureMonitorWeb.Models;
using TemperatureMonitorWeb.Models.ViewModel;
using TemperatureMonitorWeb.Repository.IRepository;

namespace TemperatureMonitorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientDetailRepository _pdRepo;
        private readonly ITempRepository _tRepo;


        public HomeController(ILogger<HomeController> logger, ITempRepository tRepo, IPatientDetailRepository pdRepo  )
        {
            _logger = logger;
            _tRepo = tRepo;
            _pdRepo = pdRepo;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM listOfPatientsAndTemps = new IndexVM()
            {
                PatientList = await _pdRepo.GetAllAsync(SD.PatientDetailsAPIPath + "all/"),
                TempList = await _tRepo.GetAllAsync(SD.TemperaturesLogEntryAPIPath),
            };
            return View(listOfPatientsAndTemps);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureMonitorWeb.Models;
using TemperatureMonitorWeb.Models.ViewModel;
using TemperatureMonitorWeb.Repository.IRepository;

namespace TemperatureMonitorWeb.Controllers
{
    [Authorize]
    public class TempsController : Controller
    {
        private readonly IPatientDetailRepository _pdRepo;
        private readonly ITempRepository _tRepo;

        public TempsController(IPatientDetailRepository pdRepo, ITempRepository tRepo)
        {
            _pdRepo = pdRepo;
            _tRepo = tRepo;
        }

        public IActionResult Index()
        {
            return View(new Temp() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<PatientDetail> pdList = await _pdRepo.GetAllAsync(SD.PatientDetailsAPIPath + "all/", HttpContext.Session.GetString("JWToken"));

            TempVM objVM = new TempVM()
            {
                PatientList = pdList.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.Name + i.Lastname,
                    Value = i.UserId.ToString()
                }),
                Temp = new Temp()

            };

            if (id == null)
            {
                // for Insert/Create
                return View(objVM);
            }

            // For Update
            objVM.Temp= await _tRepo.GetAsync(SD.TemperaturesLogEntryAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if (objVM.Temp == null)
                return NotFound();

            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TempVM obj)
        {
            if (ModelState.IsValid)
            {

                if (obj.Temp.Id==0)
                {
                    await _tRepo.CreateAsync(SD.TemperaturesLogEntryAPIPath, obj.Temp, HttpContext.Session.GetString("JWToken"));
                }
                //else
                //{
                //    await _pdRepo.UpdateAsync(SD.PatientDetailsAPIPath +  obj.UserId, obj);
                //}
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(obj);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status=   await _tRepo.DeleteAsync(SD.TemperaturesLogEntryAPIPath,id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }

            return Json(new { success = false, message = "Delete Failed" });

        }

        public async Task<IActionResult> GetAllTemp()
        {
            return Json(new { data = await _tRepo.GetAllAsync(SD.TemperaturesLogEntryAPIPath, HttpContext.Session.GetString("JWToken")) });
        }
    }
}
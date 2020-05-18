using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TemperatureMonitorWeb.Models;
using TemperatureMonitorWeb.Repository.IRepository;

namespace TemperatureMonitorWeb.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientDetailRepository _pdRepo;

        public PatientsController(IPatientDetailRepository pdRepo)
        {
            _pdRepo = pdRepo;
        }

        public IActionResult Index()
        {
            return View(new PatientDetail() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            PatientDetail p = new PatientDetail();
            if (id == null)
            {
                // for Insert/Create
                return View(p);
            }

            // For Update
            p = await _pdRepo.GetAsync(SD.PatientDetailsAPIPath+"id?Userid=", id.GetValueOrDefault());

            if (p == null)
                return NotFound();

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PatientDetail obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    obj.Picture = p1;
                }
                //else
                //{
                //    if (obj.UserId != 0)
                //    {
                //        var objfromDb = await _pdRepo.GetAsync(SD.PatientDetailsAPIPath + "id?Userid=", obj.UserId);
                //        obj.Picture = objfromDb.Picture;
                //    }
                //}

                if (obj.UserId==0)
                {
                    await _pdRepo.CreateAsync(SD.PatientDetailsAPIPath+"wu/", obj);
                }
                else
                {
                    await _pdRepo.UpdateAsync(SD.PatientDetailsAPIPath +  obj.UserId, obj);
                }
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
            var status=   await _pdRepo.DeleteAsync(SD.PatientDetailsAPIPath,id);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }

            return Json(new { success = false, message = "Delete Failed" });

        }

        public async Task<IActionResult> GetPatientDetails()
        {
            return Json(new { data = await _pdRepo.GetAllAsync(SD.PatientDetailsAPIPath+"all/") });
        }
    }
}
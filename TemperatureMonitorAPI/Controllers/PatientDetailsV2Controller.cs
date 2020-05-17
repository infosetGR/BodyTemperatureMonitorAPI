using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureMonitorAPI.Repository.IRepository;
using TemperatureMonitorAPI.Models.Dtos;
using TemperatureMonitorAPI.Models;
using AutoMapper;

namespace TemperatureMonitorAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    [ApiController]
 //   [ApiExplorerSettings(GroupName ="TemperatureMonitorOpenAPISpecPatient")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public class PatientDetailsV2Controller : ControllerBase
    {
        private readonly IPatientDetailRepository _pdRepo;
        private readonly IMapper _mapper;

        public PatientDetailsV2Controller(IPatientDetailRepository pdRepo, IMapper mapper )
        {
            _pdRepo = pdRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Patient Details
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetPatientDetailsV2")]
        public IActionResult GetPatientDetails()
        {
            int Userid = 0;
            int.TryParse(User.Identity.Name, out Userid);

            if (Userid == 0)
            {
                return BadRequest(ModelState);
            }

            var obj = _pdRepo.GetPatientDetail(Userid);
            if (obj==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PatientDetail, PatientDetailDto>(obj));
        }



    }
}
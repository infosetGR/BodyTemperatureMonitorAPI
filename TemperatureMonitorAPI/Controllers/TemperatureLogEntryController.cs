using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemperatureMonitorAPI.Repository.IRepository;
using TemperatureMonitorAPI.Models.Dtos;
using TemperatureMonitorAPI.Models;
using AutoMapper;

namespace TemperatureMonitorAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
   // [ApiExplorerSettings(GroupName = "TemperatureMonitorOpenAPISpecLog")]
  //  [Authorize]
    public class TemperatureLogEntryController : ControllerBase
    {

        private readonly ITemperatureLogEntryRepository _tRepo;
        private readonly IMapper _mapper;

        public TemperatureLogEntryController(ITemperatureLogEntryRepository tRepo, IMapper mapper)
        {
            _tRepo = tRepo;
            _mapper = mapper;
        }
        /// <summary>
        /// Get Temperature Logs
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetTempLogs")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTempLogs(int? Userid)
        {
            //int Userid = 0;
            //int.TryParse(User.Identity.Name, out Userid);
            //if ( Userid==0)
            //{
            //    return StatusCode(404, ModelState);
            //}
            
            var objList = _tRepo.GetTemperatureLogEntriesForPatient(Userid);
            var objDto = new List<TemperatureLogEntryDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TemperatureLogEntryDto>(obj));
            }
            return Ok(objDto);

        }

        /// <summary>
        /// Get Fever Logs
        /// </summary>
        /// <param name="start:date"> Start day of logs </param>
        /// <param name="end:date"> End day of logs </param>
        /// <returns></returns>
        [HttpGet("Fever/", Name = "GetFeverLogs")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTempLogs(int Userid,DateTime start, DateTime end)
        {
            //int Userid = 0;
            //int.TryParse(User.Identity.Name, out Userid);
            //if (Userid == 0)
            //{
            //    return StatusCode(404, ModelState);
            //}
            var objList = _tRepo.GetFeverLogEntriesForPatientAndPeriod(Userid, start, end);
            var objDto = new List<TemperatureLogEntryDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TemperatureLogEntryDto>(obj));
            }
            return Ok(objDto);

        }

        /// <summary>
        /// Create a Body Temperature Log
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(204, Type = typeof(TemperatureLogEntryDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult CreateTempLogs([FromBody] TemperatureLogEntryCreateDto t)
        {
            //int Userid = 0;
            //int.TryParse(User.Identity.Name, out Userid);
            //if (Userid == 0)
            //{
            //    return StatusCode(404, ModelState);
            //}

            if (t.BodyTemperatureC<35 ||  t.BodyTemperatureC>42)
            {
                ModelState.AddModelError("", "The temperature cannot be more than 42°C or less than 35°C");
                return StatusCode(500, ModelState);
            }
            var tObj = _mapper.Map<TemperatureLogEntry>(t);
          
            if (!_tRepo.CreateTemperatureLogEntry(tObj))
            {
                ModelState.AddModelError("", $"Something went wrong when creating the record {t.BodyTemperatureC}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Remove a Body Temperature Log
        /// </summary>
        /// <param name="id"> The id of the Body temperature log </param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeleteTempLog")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult DeleteTempLog(int id)
        {
            //int Userid = 0;
            //int.TryParse(User.Identity.Name, out Userid);
            //if (Userid == 0)
            //{
            //    return StatusCode(404, ModelState);
            //}

            if (!_tRepo.Exists(id))
            {
                return NotFound();
            }

            var tempObj = _tRepo.GetTemperatureLogEntry(id);
            //if (tempObj.UserId!=Userid)
            //{
            //    return NotFound();
            //}


            if (!_tRepo.DeleteTemperatureLogEntry(tempObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
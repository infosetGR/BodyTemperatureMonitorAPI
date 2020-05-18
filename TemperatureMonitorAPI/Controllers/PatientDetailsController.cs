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
    // [Route("api/[controller]")]
    [ApiController]
 //   [ApiExplorerSettings(GroupName ="TemperatureMonitorOpenAPISpecPatient")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[Authorize]
    public class PatientDetailsController : ControllerBase
    {
        private readonly IPatientDetailRepository _pdRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public PatientDetailsController(IPatientDetailRepository pdRepo, IMapper mapper, IUserRepository uRepo )
        {
            _pdRepo = pdRepo;
            _mapper = mapper;
            _userRepo = uRepo;
        }

        /// <summary>
        /// Get Patient Details by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("id/", Name = "GetPatientDetailsById")]
        public IActionResult GetPatientDetailsById(int Userid)
        {
            if (Userid == 0)
            {
                return BadRequest(ModelState);
            }

            var obj = _pdRepo.GetPatientDetail(Userid);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PatientDetail, PatientDetailDto>(obj));
        }

        /// <summary>
        /// Get Patient Details
        /// </summary>
        /// <returns></returns>
        [HttpGet( Name = "GetPatientDetails")]
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

        /// <summary>
        /// Get All Patient Details
        /// </summary>
        /// <returns></returns>
        [HttpGet("all/", Name = "GetAllPatientDetails")]
        public IActionResult GetAllPatientDetail()
        {
            //int Userid = 0;
            //int.TryParse(User.Identity.Name, out Userid);

            //if (Userid == 0)
            //{
            //    return BadRequest(ModelState);
            //}

            //var objList = _tRepo.GetFeverLogEntriesForPatientAndPeriod(Userid, start, end);
          
            //foreach (var obj in objList)
            //{
            //    objDto.Add(_mapper.Map<TemperatureLogEntryDto>(obj));
            //}
            //return Ok(objDto);

            var objList = _pdRepo.GetAllPatientDetail();
            var objDto = new List<PatientDetailDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<PatientDetailDto>(obj));
            }
            return Ok(objDto);
          
        }

        

        /// <summary>
        /// Update Patient Details
        /// </summary>
        /// <param name="pdDto"> The json object of patient details </param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "UpdatePatientDetails")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatientDetails(int id, [FromBody] PatientDetailDto pdDto)
        {
            if (pdDto == null || id != pdDto.UserId)
            {
                return BadRequest(ModelState);
            }
            //used to update only logged in user patient details
            //int Userid = 0;
            //int.TryParse(User.Identity.Name, out Userid);

            //if (pdDto == null || Userid==0)
            //{
            //    return BadRequest(ModelState);
            //}

            var obj = _mapper.Map<PatientDetail>(pdDto);
          //  obj.UserId = Userid;
            if (!_pdRepo.UpdatePatientDetail(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {pdDto}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Create Patient and User
        /// </summary>
        /// <description>Creates a patient and user with email as username</description>
        /// <returns></returns>
        
        [HttpPost("wu/", Name = "CreatePatientDetailsWithUser")]
        [ProducesResponseType(201, Type = typeof(PatientDetailDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePatientDetailsWithUser([FromBody] PatientDetailCreateDto pdDto)
        {
            
            if (pdDto == null)
            {
                return BadRequest(ModelState);
            }

            bool ifUserNameUnique = !_pdRepo.Exists(pdDto.Email);

            if (pdDto.Email!=null && !ifUserNameUnique)
            {
                return BadRequest(new { message = "This email already exists" });
            }
            var user = _userRepo.Register(pdDto.Email, "dummypassword");

            if (user == null)
            {
                return BadRequest(new { message = "Error while registering" });
            }

            var obj = _mapper.Map<PatientDetail>(pdDto);
            obj.UserId = user.Id;
            if (!_pdRepo.CreatePatientDetail(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when creating the record {user.Id}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPatientDetails", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = obj.UserId }, obj);
        }

        /// <summary>
        /// Create the Patient Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PatientDetailDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePatientDetails([FromBody] PatientDetailCreateDto pdDto)
        {
            int Userid = 0;
            int.TryParse(User.Identity.Name,out Userid);



            if (pdDto == null || Userid==0)
            {
                return BadRequest(ModelState);
            }
            if (_pdRepo.Exists(Userid))
            {
                ModelState.AddModelError("", "This user has already a profile!");
                return StatusCode(404, ModelState);
            }
           
            var obj = _mapper.Map<PatientDetail>(pdDto);
            obj.UserId = Userid;
            if (!_pdRepo.CreatePatientDetail(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when creating the record {Userid}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPatientDetails", new {version=HttpContext.GetRequestedApiVersion().ToString(), id= obj.UserId}, obj);
        }

        /// <summary>
        /// Remove the Patient Details
        /// </summary>
        /// <param name="id:int"> The id of the patient </param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "DeletePatientDetails")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePatientDetails(int id)
        {
            //int Userid = 0;
            //int.TryParse(User.Identity.Name, out Userid);

            //if (id != Userid)
            //{
            //    ModelState.AddModelError("", "You need to know your Userid to delete your profile!");
            //    return StatusCode(404, ModelState);
            //}

            if (!_pdRepo.Exists(id))
            {
                return NotFound();
            }

            var obj = _pdRepo.GetPatientDetail(id);
            if (!_pdRepo.DeletePatientDetail(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
using System.Linq.Expressions;
using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Controllers
{
    [ApiController]
    [Route("Appointments")]
    public class AppointmentController(IAppointmentRepository appointmentRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(AppointmentCreateDto appointmentCreateDto)
        {
            var email = User?.Identity?.Name!;
            try
            {
                await appointmentRepository.Create(appointmentCreateDto, email);
            }
            catch (SqlException)
            {
                return Problem();
            }
            catch (Exception)
            {
                return Problem();
            } 

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentReadDto>>> Read()
        {
            try
            {
                var email = User?.Identity?.Name!;
                var result = await appointmentRepository.Read(email);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (SqlException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update()
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] Guid appointmentId)
        {
            try
            {
                await appointmentRepository.Delete(appointmentId);
            }
            catch (SqlException)
            {
                return Problem();
            }
            catch (Exception) {
                return Problem();
            }
            return NoContent();
        }
    }
}


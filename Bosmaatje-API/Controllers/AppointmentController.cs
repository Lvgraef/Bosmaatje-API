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
            try
            {
                await appointmentRepository.Create(appointmentCreateDto);
            }
            catch (SqlException)
            {
                #if DEBUG
                    throw;
                #endif 
                    return Problem();
            }

            return CreatedAtRoute("Read", null, appointmentCreateDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentReadDto>>> Read()
        {
            var email = User?.Identity?.Name!;
            var result = await appointmentRepository.Read(email);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
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
                #if DEBUG
                    throw;
                #endif 
                    return Problem();
            }
            return NoContent();
        }
    }
}


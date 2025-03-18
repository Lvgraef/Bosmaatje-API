using Bosmaatje_API.Dto;
using Bosmaatje_API.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Controllers
{
    [ApiController]
    [Route("Treatments")]
    public class TreatmentController(ITreatmentRepository treatmentRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ConfigurationReadDto>> Read()
        {
            var email = User?.Identity?.Name!;
            var result = await treatmentRepository.Read(email);
            if(result == null)
            { 
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromQuery] Guid treatmentId, TreatmentUpdateDto treatmentUpdateDto)
        {
            try
            {
                var email = User?.Identity?.Name!;
                await treatmentRepository.Update(treatmentUpdateDto, treatmentId);
            }
            catch (SqlException exception)
            {
                
                if (exception.Number == 547)
                {
                    return Conflict();
                }
                #if DEBUG
                    throw;
                #endif 
                return Problem();
            }
            return NoContent();
        }
    } 
}


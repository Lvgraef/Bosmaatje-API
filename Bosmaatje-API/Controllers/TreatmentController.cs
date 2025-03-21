using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Controllers
{
    [ApiController]
    [Route("Treatments")]
    public class TreatmentController(ITreatmentRepository treatmentRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TreatmentReadDto>>> Read([FromQuery] string treatmentPlanName)
        {
            var email = User?.Identity?.Name!;
            var result = await treatmentRepository.Read(email, treatmentPlanName);
            if(result == null)
            { 
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromQuery] string treatmentId, TreatmentUpdateDto treatmentUpdateDto, [FromQuery] string treatmentPlanName)
        {
            try
            {
                var email = User?.Identity?.Name!;
                await treatmentRepository.Update(treatmentUpdateDto, treatmentId, email, treatmentPlanName);
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


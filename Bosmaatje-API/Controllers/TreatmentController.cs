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
        public async Task<ActionResult<List<TreatmentReadDto>>> Read([FromQuery] string? treatmentPlanName)
        {
            try
            {
                var email = User?.Identity?.Name!;
                var result = await treatmentRepository.Read(email, treatmentPlanName);
                return Ok(result);
            }
            catch (Exception) 
            {
                    return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromQuery] Guid treatmentId, TreatmentUpdateDto treatmentUpdateDto)
        {
            try
            {
                var email = User?.Identity?.Name!;
                await treatmentRepository.Update(treatmentUpdateDto, treatmentId, email);
            }
            catch (Exception)
            {
                return Problem();
            }
            return NoContent();
        }
    } 
}


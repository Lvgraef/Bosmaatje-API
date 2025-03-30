using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Controllers
{
    [ApiController]
    [Route("Configurations")]
    public class ConfigurationController(IConfigurationRepository configurationRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(ConfigurationCreateDto configurationCreateDto)
        {
            try
            {
                var email = User?.Identity?.Name!;
                var conflict = await configurationRepository.ConflictCheck(email);
                if (conflict)
                {
                    return Conflict();
                }
                await configurationRepository.Create(configurationCreateDto, email);
            }
            catch (SqlException)
            {
            #if DEBUG
                throw;
            #endif
                return Problem();
            }

            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<ConfigurationReadDto>> Read()
        {
            var email = User?.Identity?.Name!;
            var result = await configurationRepository.Read(email);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(ConfigurationUpdateDto configurationUpdateDto)
        {
            try
            {
                var email = User?.Identity?.Name!;
                await configurationRepository.Update(configurationUpdateDto, email);
                return NoContent();
            }
            catch (SqlException)
            {
            #if DEBUG
                throw;
            #endif
                return Problem();
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            try
            {
                var email = User?.Identity?.Name!;
                await configurationRepository.Delete(email);
                return NoContent();
            }

            catch (SqlException)
            {
                #if DEBUG
                    throw;
                #endif 
                return Problem(); 
            }
        }
    }
}


using Bosmaatje_API.Dto;
using Bosmaatje_API.IRepository;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bosmaatje_API.Controllers
{
    [ApiController]
    [Route("Configurations")]
    public class ConfigurationController(IConfigurationRepository configurationRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(ConfigurationCreateDto configurationCreateDto)
        {
            var email = User?.Identity?.Name;
            var successful = await configurationRepository.Create(configurationCreateDto, email);
            if(!successful)
            {
                return Problem("Configuration creation failed.");
            }
            return CreatedAtRoute("Read", null, configurationCreateDto);
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
            var email = User?.Identity?.Name!;
            var successful = await configurationRepository.Update(configurationUpdateDto, email);
            if(!successful)
            {
                return Problem("Updating configuration failed");
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            var email = User?.Identity?.Name!;
            var successful = await configurationRepository.Delete(email);
            if(!successful)
            {
                return Problem("Deleting configuration failed");
            }
            return NoContent();
        }
    }
}


using System;
using Models;
using Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Controllers
{
    [ApiController]
    [Route("Configurations")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationRepository _configurationRepository;

        public ConfigurationController(ConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Create(ConfigurationCreateDto configurationCreateDto)
        {
            var Email = User?.Identity?.name!;
            var Succesfull = await _configurationRepository.Create(configurationCreateDto, Email);
            if(!Succesfull)
            {
                return ServerError("Configuration creation failed.");
            }
            return CreatedAtRoute("Read", null, configurationCreateDto);
        }

        [HttpGet]
        public async Task<ActionResult<ConfigurationReadDto>> Read()
        {
            var Email = User?.Identity?.name!;
            var Result = await _configurationRepository.Read(Email);
            if(Result == null)
            {
                return NotFound();
            }
            return Ok(Result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(ConfigurationUpdateDto configurationUpdateDto)
        {
            var Email = User?.Identity?.name!;
            var Succesfull = await _configurationRepository.Update(configurationUpdateDto, Email);
            if(!Succesfull)
            {
                return ServerError("Updating configuration failed");
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            var Email = User?.Identity?.name!;
            var Succesfull = await _configurationRepository.Delete(Email);
            if(!Succesfull)
            {
                return ServerError("Deleting configuration failed");
            }
            return NoContent();
        }
    }
}


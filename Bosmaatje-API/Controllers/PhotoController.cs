using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Controllers
{
    [ApiController]
    [Route("Photos")]
    public class PhotoController(IPhotoRepository photoRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(PhotoCreateDto photoCreateDto)
        {
            try
            {
                await photoRepository.Create(photoCreateDto);
            }
            catch (SqlException)
            {
                #if DEBUG
                    throw;
                #endif
                    return Problem();
            }

            return CreatedAtRoute("Read", null, photoCreateDto);
        }

        [HttpGet]
        public async Task<ActionResult<PhotoReadDto>> Read()
        {
                var email = User?.Identity?.Name!;
                var result = await photoRepository.Read(email);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            try
            {
                var email = User?.Identity?.Name!;
                await photoRepository.Delete(email);
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
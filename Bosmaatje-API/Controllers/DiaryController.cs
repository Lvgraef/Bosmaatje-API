using System.Reflection.Metadata.Ecma335;
using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Controllers
{
    [ApiController]
    [Route("Diaries")]
    public class DiaryController(IDiaryRepository diaryRepository) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(DiaryCreateDto diaryCreateDto)
        {
            try
            {
                var email = User?.Identity?.Name!;
                await diaryRepository.Create(diaryCreateDto, email);
            }
            catch (Exception)
            {
                return Problem();
            }
            return Created();

        }

        [HttpGet]
        public async Task<ActionResult<List<DiaryReadDto>>> Read([FromQuery] DateTime? date)
        {
            try
            {
                var email = User?.Identity?.Name!;
                var result = await diaryRepository.Read(email, date);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                    return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult> Update(DiaryUpdateDto diaryUpdateDto, [FromQuery] DateTime date)
        {
            try
            {
                var email = User?.Identity?.Name!;
                await diaryRepository.Update(diaryUpdateDto, email, date);
                return NoContent();
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DateTime date)
        {
            try
            {
                var email = User?.Identity?.Name!;
                await diaryRepository.Delete(email, date);
                return NoContent();
            }

            catch (Exception)
            {
                return Problem(); 
            }
        }
    }
}
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
            catch (SqlException)
            {
                #if DEBUG
                    throw;
                #endif
                return Problem();
            }
            return CreatedAtRoute("Read", null, diaryCreateDto);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<DiaryReadDto>>> Read([FromQuery] DateTime? date)
        {
            var email = User?.Identity?.Name!;
            var result = await diaryRepository.Read(email, date);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
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
            catch (SqlException)
            {
                #if DEBUG
                    throw;
                #endif
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
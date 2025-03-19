using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Bosmaatje_API.Controllers;

public class DiaryController
{
    [ApiController]
    [Route("Diaries")]
    public class ConfigurationController(IDiaryRepository diaryRepository) : ControllerBase
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
            return CreatedAtRoute("Create", null, diaryCreateDto);
        }
        
        [HttpGet]
        public async Task<ActionResult<DiaryReadDto>> Read()
        {
            var email = User?.Identity?.Name!;
            var result = await diaryRepository.Read(email);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(DiaryUpdateDto diaryUpdateDto)
        {
            try
            {
                var email = User?.Identity?.Name!;
                await diaryRepository.Update(diaryUpdateDto, email);
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
                await diaryRepository.Delete(email);
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
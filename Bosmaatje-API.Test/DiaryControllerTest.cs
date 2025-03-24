using Bosmaatje_API.Controllers;
using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Bosmaatje_API.Test;

public class DiaryControllerTest
{
     private DateTime date = DateTime.MinValue;
     private List<DiaryReadDto> EmptyDiaryReadDtoList = new List<DiaryReadDto>();
     private static readonly DiaryCreateDto EmptyDiaryCreateDto = new()
     {
          date = DateTime.MinValue,
          content = "",
          email = ""
     };
     
     private static readonly DiaryReadDto EmptyDiaryReadDto = new()
     {
          date = "",
          content = ""
     };
     
     private static readonly DiaryUpdateDto EmptyDiaryUpdateDto = new()
     {
          content = null
     };
     
     public async Task Create_CreateDiary_CreatedAtRoute()
     {
          var mockDiaryRepository = new Mock<IDiaryRepository>();
          mockDiaryRepository.Setup(repo => repo.Create(It.IsAny<DiaryCreateDto>(), It.IsAny<string>())).Returns(Task.CompletedTask);
          var controller = new DiaryController(mockDiaryRepository.Object);
          var result = await controller.Create(EmptyDiaryCreateDto);
          Assert.IsType<CreatedAtRouteResult>(result);
     }
     public async Task Read_ReadDiary_NotFound()
     {
          var mockDiaryRepository = new Mock<IDiaryRepository>();
          mockDiaryRepository.Setup(repo => repo.Read(It.IsAny<string>(), It.IsAny<DateTime>()))!.ReturnsAsync((List<DiaryReadDto>)null);
          var controller = new DiaryController(mockDiaryRepository.Object);
          var result = await controller.Read(date);
          Assert.IsType<NotFoundResult>(result.Result);
     }
     
     public async Task Read_ReadDiary_Ok()
     {
          var mockDiaryRepository = new Mock<IDiaryRepository>();
          mockDiaryRepository.Setup(repo => repo.Read(It.IsAny<string>(), It.IsAny<DateTime>()))!.ReturnsAsync(EmptyDiaryReadDtoList);
          var controller = new DiaryController(mockDiaryRepository.Object);
          var result = await controller.Read(date);
          Assert.IsType<OkObjectResult>(result.Result);
     }

     public async Task Update_UpdateDiary_NoContent()
     {
          var mockDiaryRepository = new Mock<IDiaryRepository>();
          mockDiaryRepository.Setup(repo => repo.Update(It.IsAny<DiaryUpdateDto>(), It.IsAny<string>())).Returns(Task.CompletedTask);
          var controller = new DiaryController(mockDiaryRepository.Object);
          var result = await controller.Update(EmptyDiaryUpdateDto);
          Assert.IsType<NoContentResult>(result);
     }
     
     public async Task Delete_DeleteDiary_NoContent()
     {
          var mockDiaryRepository = new Mock<IDiaryRepository>();
          mockDiaryRepository.Setup(repo => repo.Delete(It.IsAny<string>())).Returns(Task.CompletedTask);
          var controller = new DiaryController(mockDiaryRepository.Object);
          var result = await controller.Delete();
          Assert.IsType<NoContentResult>(result);
     }
}
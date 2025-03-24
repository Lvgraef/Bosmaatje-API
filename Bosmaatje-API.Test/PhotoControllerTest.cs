using Bosmaatje_API.Controllers;
using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Bosmaatje_API.Test
{
    public class PhotoControllerTest
    {
        private static readonly PhotoCreateDto EmptyPhotoCreateDto = new()
        {
            photoId = Guid.Empty,
            photoPath = "",
            email = ""
        };
        
        private static readonly PhotoReadDto EmptyPhotoReadDto = new()
        {
            photoId = Guid.Empty,
            photoPath = ""
        };
        
        [Fact]
        public async Task Create_CreatePhoto_CreatedAtRoute()
        {
            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(repo => repo.Create(It.IsAny<PhotoCreateDto>())).Returns(Task.CompletedTask);
            var controller = new PhotoController(mockPhotoRepository.Object);
            var result = await controller.Create(EmptyPhotoCreateDto);
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task Read_ReadPhoto_NotFound()
        {
            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(repo => repo.Read(It.IsAny<string>()))!. ReturnsAsync((PhotoReadDto)null);
            var controller = new PhotoController(mockPhotoRepository.Object);
            var result = await controller.Read();
            Assert.IsType<NotFoundResult>(result.Result);
        }
        
        [Fact]
        public async Task Read_ReadPhoto_Ok()
        {
            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(repo => repo.Read(It.IsAny<string>()))!. ReturnsAsync(EmptyPhotoReadDto);
            var controller = new PhotoController(mockPhotoRepository.Object);
            var result = await controller.Read();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_DeletePhoto_NoContent()
        {
            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(repo => repo.Delete(It.IsAny<string>()))!.Returns(Task.CompletedTask);
            var controller = new PhotoController(mockPhotoRepository.Object);
            var result = await controller.Delete();
            Assert.IsType<NoContentResult>(result);
        }
    }
}
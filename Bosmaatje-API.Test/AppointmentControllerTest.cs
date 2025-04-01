using System.Reflection.Metadata;
using Bosmaatje_API.Controllers;
using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Bosmaatje_API.Test
{
    public class AppointmentControllerTest
    {
        private List<AppointmentReadDto> appointments = new List<AppointmentReadDto>();
        public static readonly AppointmentCreateDto EmptyAppointmentCreateDto = new()
        {
            name = "",
            date = DateTime.MinValue
        };
      
        [Fact]
        public async Task Create_CreateAppointment_Created()
        {
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            mockAppointmentRepository.Setup(repo => repo.Create(It.IsAny<AppointmentCreateDto>(),It.IsAny<string>())).Returns(Task.CompletedTask);
            var controller = new AppointmentController(mockAppointmentRepository.Object);
            var result = await controller.Create(EmptyAppointmentCreateDto);
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_AppointmentThrowGeneralException()
        {
            // Arrange
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            mockAppointmentRepository.Setup(repo => repo.Create(It.IsAny<AppointmentCreateDto>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new AppointmentController(mockAppointmentRepository.Object);

            // Act
            var result = await controller.Create(EmptyAppointmentCreateDto);

            // Assert: check if the result is a Problem (which is an ObjectResult with a 500 status code)
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task Read_ReadAppointment_Ok()
        {
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            mockAppointmentRepository.Setup(repo => repo.Read(It.IsAny<string>())).ReturnsAsync(appointments);
            var controller = new AppointmentController(mockAppointmentRepository.Object);
            var result = await controller.Read();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Read_ReadAppointment_Notfound()
        {
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            mockAppointmentRepository.Setup(repo => repo.Read(It.IsAny<string>()))!.ReturnsAsync((List<AppointmentReadDto>)null);
            var controller = new AppointmentController(mockAppointmentRepository.Object);
            var result = await controller.Read();
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Read_AppointmetThrowGeneralException_BadRequest()
        {
            // Arrange
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            mockAppointmentRepository.Setup(repo => repo.Read(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new AppointmentController(mockAppointmentRepository.Object);

            // Act
            var result = await controller.Read();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_UpdateAppointment_MethodNotAllowed()
        {
            // Arrange
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            var controller = new AppointmentController(mockAppointmentRepository.Object);

            // Act
            var result =  controller.Update();

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status405MethodNotAllowed, ((StatusCodeResult)result).StatusCode);
        }


        [Fact]
        public async Task Delete_DeleteAppointment_NoContent()
        {
            var mockAppointRepository = new Mock<IAppointmentRepository>();
            mockAppointRepository.Setup(repo => repo.Delete(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            var controller = new AppointmentController(mockAppointRepository.Object);
            var result = await controller.Delete(Guid.Empty);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_AppoinmentThrowGeneralException_Problem()
        {
            // Arrange
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            mockAppointmentRepository.Setup(repo => repo.Delete(It.IsAny<Guid>())).ThrowsAsync(new Exception());
            var controller = new AppointmentController(mockAppointmentRepository.Object);

            // Act
            var result = await controller.Delete(Guid.Empty);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}

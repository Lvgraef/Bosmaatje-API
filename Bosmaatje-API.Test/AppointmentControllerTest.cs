using System.Reflection.Metadata;
using Bosmaatje_API.Controllers;
using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
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
            Assert.IsType<CreatedAtRouteResult>(result);
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
        public async Task Read_ReadAppointment_Ok()
        {
            var mockAppointmentRepository = new Mock<IAppointmentRepository>();
            mockAppointmentRepository.Setup(repo => repo.Read(It.IsAny<string>())).ReturnsAsync(appointments);
            var controller = new AppointmentController(mockAppointmentRepository.Object);
            var result = await controller.Read();
            Assert.IsType<OkObjectResult>(result);
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
    }
}

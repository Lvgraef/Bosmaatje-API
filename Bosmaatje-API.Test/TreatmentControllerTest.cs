using Bosmaatje_API.Controllers;
using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TreatmentController = Bosmaatje_API.Controllers.TreatmentController;

namespace Bosmaatje_API.Test
{
    public class TreatmentControllerTest
    {
        private Guid treatmentId = Guid.NewGuid();
        private string _treatmentPlanName = "";
        private List<TreatmentReadDto> treatmentReadDtoList = new List<TreatmentReadDto>();
        private static readonly TreatmentUpdateDto EmptyTreatmentUpdateDto = new()
        {
            date = DateTime.MinValue,
            doctorName = "",
            stickerId = "",
        };


        [Fact]
        public async Task Read_ReadTreatment_Ok()
        {
            var mockTreatmentRepository = new Mock<ITreatmentRepository>();
            mockTreatmentRepository.Setup(repo => repo.Read(It.IsAny<string>(), It.IsAny<string>()))!.ReturnsAsync(treatmentReadDtoList);
            var controller = new TreatmentController(mockTreatmentRepository.Object);
            var result = await controller.Read(_treatmentPlanName);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Read_TreatmentThrowGeneralException_Problem()
        {
            // Arrange
            var mockTreatmentRepository = new Mock<ITreatmentRepository>();
            mockTreatmentRepository.Setup(repo => repo.Read(It.IsAny<string>(), It.IsAny<string>()))!.ThrowsAsync(new Exception());
            var controller = new TreatmentController(mockTreatmentRepository.Object);
            //Act
            var result = await controller.Read(_treatmentPlanName);
            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.Equal(400, ((BadRequestResult)result.Result).StatusCode);
        }


        [Fact]
        public async Task Update_UpdateTreatment_NoContent()
        {
            var mockTreatmentRepository = new Mock<ITreatmentRepository>();
            mockTreatmentRepository.Setup(repo => repo.Update(It.IsAny<TreatmentUpdateDto>(), It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            var controller = new TreatmentController(mockTreatmentRepository.Object);
            var result = await controller.Update(treatmentId, EmptyTreatmentUpdateDto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_TreatmentThrowGeneralException_Problem()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<ITreatmentRepository>();
            mockConfigurationRepository.Setup(repo => repo.Update(It.IsAny<TreatmentUpdateDto>(), It.IsAny<Guid>(),  It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new TreatmentController(mockConfigurationRepository.Object);
            // Act
            var result = await controller.Update(treatmentId, EmptyTreatmentUpdateDto);
            // Assert
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

    }
}

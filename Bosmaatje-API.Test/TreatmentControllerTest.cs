using Bosmaatje_API.Dto;
using Bosmaatje_API.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TreatmentController = Bosmaatje_API.Controllers.TreatmentController;

namespace Bosmaatje_API.Test
{
    public class TreatmentControllerTest
    {
        private string _treatmentPlanName = "";
        private List<TreatmentReadDto> treatmentReadDtoList = new List<TreatmentReadDto>();
        private static readonly TreatmentUpdateDto EmptyTreatmentUpdateDto = new()
        {
            date = DateTime.MinValue,
            doctorName = ""
        };

        [Fact]
        public async Task Read_ReadTreatment_NotFound()
        {
            var mockTreatmentRepository = new Mock<ITreatmentRepository>();
            mockTreatmentRepository.Setup(repo => repo.Read(It.IsAny<string>(), It.IsAny<string>()))!.ReturnsAsync((List<TreatmentReadDto>)null);
            var controller = new TreatmentController(mockTreatmentRepository.Object);
            var result = await controller.Read(_treatmentPlanName);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Read_ReadConfiguration_Ok()
        {
            var mockTreatmentRepository = new Mock<ITreatmentRepository>();
            mockTreatmentRepository.Setup(repo => repo.Read(It.IsAny<string>(), It.IsAny<string>()))!.ReturnsAsync(treatmentReadDtoList);
            var controller = new TreatmentController(mockTreatmentRepository.Object);
            var result = await controller.Read(_treatmentPlanName);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_UpdateConfiguration_NoContent()
        {
            var mockTreatmentRepository = new Mock<ITreatmentRepository>();
            mockTreatmentRepository.Setup(repo => repo.Update(It.IsAny<TreatmentUpdateDto>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            var controller = new TreatmentController(mockTreatmentRepository.Object);
            var result = await controller.Update(_treatmentPlanName, EmptyTreatmentUpdateDto);
            Assert.IsType<NoContentResult>(result);
        }
    }
}

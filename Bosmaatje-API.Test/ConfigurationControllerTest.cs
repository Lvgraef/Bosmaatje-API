using Bosmaatje_API.Controllers;
using Bosmaatje_API.Dto;
using Bosmaatje_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ConfigurationController = Bosmaatje_API.Controllers.ConfigurationController;

namespace Bosmaatje_API.Test
{
    public class ConfigurationControllerTest
    {

        private static readonly ConfigurationCreateDto EmptyConfigurationCreateDto = new()
        {
            childName = "",
            childBirthDate = DateTime.MinValue,
            primaryDoctorName = "",
            characterId = "",
            treatmentPlanName = ""
        };
        
        private static readonly ConfigurationReadDto EmptyConfigurationReadDto = new()
        {
            configurationId = Guid.Empty,
            childName = "",
            childBirthDate = DateTime.MinValue,
            primaryDoctorName = "",
            characterId = "",
            treatmentPlanName = ""
        };

        private static readonly ConfigurationUpdateDto EmptyConfigurationUpdateDto = new()
        {
            primaryDoctorName = "",
            characterId = "",
            treatmentPlanName = null
        };

        [Fact]
        public async Task Create_CreateConfiguration_Conflict()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.ConflictCheck(It.IsAny<string>())).ReturnsAsync(true);
            var controller = new ConfigurationController(mockConfigurationRepository.Object);

            // Act
            var result = await controller.Create(EmptyConfigurationCreateDto);

            // Assert
            Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public async Task Create_CreateConfiguration_CreatedAtRoute()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.ConflictCheck(It.IsAny<string>())).ReturnsAsync(false);
            mockConfigurationRepository.Setup(repo => repo.Create(It.IsAny<ConfigurationCreateDto>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            var controller = new ConfigurationController(mockConfigurationRepository.Object);

            // Act
            var result = await controller.Create(EmptyConfigurationCreateDto);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Create_ConfigurationThrowGeneralException_CreatedAtRoute()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.ConflictCheck(It.IsAny<string>())).ReturnsAsync(false);
            mockConfigurationRepository.Setup(repo => repo.Create(It.IsAny<ConfigurationCreateDto>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new ConfigurationController(mockConfigurationRepository.Object);

            // Act
            var result = await controller.Create(EmptyConfigurationCreateDto);

            // Assert
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }



        [Fact]
        public async Task Read_ReadConfiguration_NotFound()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.Read(It.IsAny<string>())).ReturnsAsync((ConfigurationReadDto)null);
            var controller = new ConfigurationController(mockConfigurationRepository.Object);

            // Act
            var result = await controller.Read();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Read_ReadConfiguration_Ok()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.Read(It.IsAny<string>())).ReturnsAsync(EmptyConfigurationReadDto);
            var controller = new ConfigurationController(mockConfigurationRepository.Object);

            // Act
            var result = await controller.Read();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Read_ConfigurationThrowGeneralException_Problem()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.Read(It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new ConfigurationController(mockConfigurationRepository.Object);
            // Act
            var result = await controller.Read();
            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.Equal(400, ((BadRequestResult)result.Result).StatusCode);
        }



        [Fact]
        public async Task Update_UpdateConfiguration_NoContent()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.Update(It.IsAny<ConfigurationUpdateDto>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            var controller = new ConfigurationController(mockConfigurationRepository.Object);

            // Act
            var result = await controller.Update(EmptyConfigurationUpdateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ConfigurationThrowGeneralException_Problem()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.Update(It.IsAny<ConfigurationUpdateDto>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            var controller = new ConfigurationController(mockConfigurationRepository.Object);
            // Act
            var result = await controller.Update(EmptyConfigurationUpdateDto);
            // Assert
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }


        [Fact]
        public async Task Delete_DeleteConfiguration_NoContent()
        {
            // Arrange
            var mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockConfigurationRepository.Setup(repo => repo.Delete(It.IsAny<string>())).Returns(Task.CompletedTask);
            var controller = new ConfigurationController(mockConfigurationRepository.Object);

            // Act
            var result = await controller.Delete();
        }



    }
}


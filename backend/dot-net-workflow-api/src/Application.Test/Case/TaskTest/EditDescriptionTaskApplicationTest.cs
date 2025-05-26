using Application.Case.Task.EditDescriptionTask;
using Domain.Case.Task.EditDescriptionTask;
using Domain.Entities.Task;
using Moq;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskTest
{
    public class EditDescriptionTaskApplicationTest
    {
        private readonly Mock<IEditDescriptionTaskProvider> _providerMock;
        private readonly EditDescriptionTaskApplication _application;

        public EditDescriptionTaskApplicationTest()
        {
            _providerMock = new Mock<IEditDescriptionTaskProvider>();
            _application = new EditDescriptionTaskApplication(_providerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenParamIsNull()
        {
            // Act
            var result = await _application.Execute(null);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid param", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenParamIsInvalid()
        {
            // Arrange
            var invalidDomain = new EditDescriptionTaskDomain { Id = Guid.NewGuid() };
            // Assuming IsValidDomain is a property that determines if the domain is valid
            var domain = new TestEditDescriptionTaskDomain { Id = invalidDomain.Id, Description = invalidDomain.Description, IsValidDomain = true };

            // Act
            var result = await _application.Execute(domain);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid param", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldCallProviderAndReturnResult_WhenParamIsValid()
        {
            // Arrange
            var validDomain = new TestEditDescriptionTaskDomain { Id = Guid.NewGuid(), Description = "desc", IsValidDomain = false };
            var expectedTask = new TaskDomain();
            var expectedResult = expectedTask.GetResultDetailSuccess();

            _providerMock
                .Setup(p => p.EditDescriptionTaskAsync(validDomain))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _application.Execute(validDomain);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedTask, result.ResultData);
            _providerMock.Verify(p => p.EditDescriptionTaskAsync(validDomain), Times.Once);
        }

        // This class is used to simulate the behavior of the EditDescriptionTaskDomain for testing purposes.
        private class TestEditDescriptionTaskDomain : EditDescriptionTaskDomain
        {
            public bool IsValidDomain { get; set; }
        }
    }
}

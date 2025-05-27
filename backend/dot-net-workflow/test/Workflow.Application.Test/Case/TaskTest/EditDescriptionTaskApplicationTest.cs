using Workflow.Application.Case.Task.EditDescriptionTask.UpdateTask;
using Workflow.Domain.Case.Task.UpdateTask;
using Workflow.Domain.Entities.Task;
using Moq;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskTest
{
    public class EditDescriptionTaskApplicationTest
    {
        private readonly Mock<IUpdateTaskProvider> _providerMock;
        private readonly UpdateTaskApplication _application;

        public EditDescriptionTaskApplicationTest()
        {
            _providerMock = new Mock<IUpdateTaskProvider>();
            _application = new UpdateTaskApplication(_providerMock.Object);
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
            var invalidDomain = new UpdateTaskDomain { Id = Guid.NewGuid() };
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
                .Setup(p => p.UpdateTaskAsync(validDomain))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _application.Execute(validDomain);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedTask, result.ResultData);
            _providerMock.Verify(p => p.UpdateTaskAsync(validDomain), Times.Once);
        }

        // This class is used to simulate the behavior of the UpdateTaskDomain for testing purposes.
        private class TestEditDescriptionTaskDomain : UpdateTaskDomain
        {
            public bool IsValidDomain { get; set; }
        }
    }
}

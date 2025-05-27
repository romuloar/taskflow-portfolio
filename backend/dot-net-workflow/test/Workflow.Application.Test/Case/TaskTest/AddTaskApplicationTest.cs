using Workflow.Application.Case.Task.AddTask;
using Workflow.Domain.Case.Task.AddTask;
using Workflow.Domain.Entities.Task;
using Moq;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System.Threading.Tasks;

namespace Application.Test.Case.TaskTest
{
    public class AddTaskApplicationTest
    {
        private readonly Mock<IAddTaskProvider> _providerMock;
        private readonly AddTaskApplication _application;

        public AddTaskApplicationTest()
        {
            _providerMock = new Mock<IAddTaskProvider>();
            _application = new AddTaskApplication(_providerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenAddTaskIsNull()
        {
            // Act
            var result = await _application.Execute(null);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task is required", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenDomainIsInvalid()
        {
            // Arrange
            var invalidDomain = new AddTaskDomain { Description = "" };            
            // Act
            var result = await _application.Execute(invalidDomain);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid domain", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldCallProviderAndReturnResult_WhenDomainIsValid()
        {
            // Arrange
            var validDomain = new AddTaskDomain { Description = "Valid Task" };            

            var expectedTask = new TaskDomain();
            var expectedResult = expectedTask.GetResultDetailSuccess();

            _providerMock
                .Setup(p => p.AddTaskAsync(validDomain))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _application.Execute(validDomain);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedTask, result.ResultData);
            _providerMock.Verify(p => p.AddTaskAsync(validDomain), Times.Once);
        }
    }
}

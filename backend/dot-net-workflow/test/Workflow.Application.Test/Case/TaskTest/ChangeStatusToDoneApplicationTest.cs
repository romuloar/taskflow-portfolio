using Workflow.Application.Case.Task.ChangeStatusToDone;
using Workflow.Domain.Case.Task.ChangeStatusToDone;
using Workflow.Domain.Case.Task.GetTask;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Moq;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskTest
{
    public class ChangeStatusToDoneApplicationTest
    {
        private readonly Mock<IChangeStatusToDoneProvider> _providerMock;
        private readonly Mock<IGetTaskByIdProvider> _getTaskByIdMock;
        private readonly ChangeStatusToDoneApplication _application;

        public ChangeStatusToDoneApplicationTest()
        {
            _providerMock = new Mock<IChangeStatusToDoneProvider>();
            _getTaskByIdMock = new Mock<IGetTaskByIdProvider>();
            _application = new ChangeStatusToDoneApplication(_providerMock.Object, _getTaskByIdMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenIdIsEmpty()
        {
            // Act
            var result = await _application.Execute(Guid.Empty);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid id", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenTaskNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _getTaskByIdMock
                .Setup(p => p.GetTaskByIdAsync(id))
                .ReturnsAsync((ResultDetail<TaskDomain>)null);

            // Act
            var result = await _application.Execute(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task not found", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenTaskAlreadyDone()
        {
            // Arrange
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Status = EnumTaskStatus.Done };
            var taskResult = task.GetResultDetailSuccess();

            _getTaskByIdMock
                .Setup(p => p.GetTaskByIdAsync(id))
                .ReturnsAsync(taskResult);

            // Act
            var result = await _application.Execute(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task already done", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldCallProviderAndReturnResult_WhenValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Status = EnumTaskStatus.InProgress };
            var taskResult = task.GetResultDetailSuccess();
            var expectedResult = new TaskDomain { Id = id, Status = EnumTaskStatus.Done }.GetResultDetailSuccess();

            _getTaskByIdMock
                .Setup(p => p.GetTaskByIdAsync(id))
                .ReturnsAsync(taskResult);

            _providerMock
                .Setup(p => p.ChangeStatusToDoneAsync(id))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _application.Execute(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedResult.ResultData, result.ResultData);
            _providerMock.Verify(p => p.ChangeStatusToDoneAsync(id), Times.Once);
        }
    }
}

using Application.Case.Task.ChangeStatusToInProgress;
using Domain.Case.Task.ChangeStatusToInProgress;
using Domain.Case.Task.GetTask;
using Domain.Entities.Task;
using Domain.Generic.Task;
using Moq;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskTest
{
    public class ChangeStatusToInProgressApplicationTest
    {
        private readonly Mock<IChangeStatusToInProgressProvider> _providerMock;
        private readonly Mock<IGetTaskByIdProvider> _getTaskByIdMock;
        private readonly ChangeStatusToInProgressApplication _application;

        public ChangeStatusToInProgressApplicationTest()
        {
            _providerMock = new Mock<IChangeStatusToInProgressProvider>();
            _getTaskByIdMock = new Mock<IGetTaskByIdProvider>();
            _application = new ChangeStatusToInProgressApplication(_providerMock.Object, _getTaskByIdMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenIdIsEmpty()
        {
            var result = await _application.Execute(Guid.Empty);
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid id", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenTaskNotFound()
        {
            var id = Guid.NewGuid();
            _getTaskByIdMock
                .Setup(p => p.GetTaskByIdAsync(id))
                .ReturnsAsync((ResultDetail<TaskDomain>)null);

            var result = await _application.Execute(id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Task not found", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenTaskAlreadyInProgress()
        {
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Status = EnumTaskStatus.InProgress };
            var taskResult = task.GetResultDetailSuccess();

            _getTaskByIdMock
                .Setup(p => p.GetTaskByIdAsync(id))
                .ReturnsAsync(taskResult);

            var result = await _application.Execute(id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Task already in progress", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldCallProviderAndReturnResult_WhenValid()
        {
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Status = EnumTaskStatus.New };
            var taskResult = task.GetResultDetailSuccess();
            var expectedResult = new TaskDomain { Id = id, Status = EnumTaskStatus.InProgress }.GetResultDetailSuccess();

            _getTaskByIdMock
                .Setup(p => p.GetTaskByIdAsync(id))
                .ReturnsAsync(taskResult);

            _providerMock
                .Setup(p => p.ChangeStatusToInProgressAsync(id))
                .ReturnsAsync(expectedResult);

            var result = await _application.Execute(id);

            Assert.True(result.IsSuccess);
            Assert.Equal(expectedResult.ResultData, result.ResultData);
            _providerMock.Verify(p => p.ChangeStatusToInProgressAsync(id), Times.Once);
        }
    }
}

using Workflow.Application.Case.Task.ChangeStatusToImpediment;
using Workflow.Domain.Case.Task.ChangeStatusToImpediment;
using Workflow.Domain.Case.Task.GetTask;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Moq;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskTest
{
    public class ChangeStatusToImpedimentApplicationTest
    {
        private readonly Mock<IChangeStatusToImpedimentProvider> _providerMock;
        private readonly Mock<IGetTaskByIdProvider> _getTaskByIdMock;
        private readonly ChangeStatusToImpedimentApplication _application;

        public ChangeStatusToImpedimentApplicationTest()
        {
            _providerMock = new Mock<IChangeStatusToImpedimentProvider>();
            _getTaskByIdMock = new Mock<IGetTaskByIdProvider>();
            _application = new ChangeStatusToImpedimentApplication(_providerMock.Object, _getTaskByIdMock.Object);
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
        public async Task Execute_ShouldReturnError_WhenTaskAlreadyImpediment()
        {
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Status = EnumTaskStatus.Impediment };
            var taskResult = task.GetResultDetailSuccess();

            _getTaskByIdMock
                .Setup(p => p.GetTaskByIdAsync(id))
                .ReturnsAsync(taskResult);

            var result = await _application.Execute(id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Task already impediment", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldCallProviderAndReturnResult_WhenValid()
        {
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Status = EnumTaskStatus.InProgress };
            var taskResult = task.GetResultDetailSuccess();
            var expectedResult = new TaskDomain { Id = id, Status = EnumTaskStatus.Impediment }.GetResultDetailSuccess();

            _getTaskByIdMock
                .Setup(p => p.GetTaskByIdAsync(id))
                .ReturnsAsync(taskResult);

            _providerMock
                .Setup(p => p.ChangeStatusToImpedimentAsync(id))
                .ReturnsAsync(expectedResult);

            var result = await _application.Execute(id);

            Assert.True(result.IsSuccess);
            Assert.Equal(expectedResult.ResultData, result.ResultData);
            _providerMock.Verify(p => p.ChangeStatusToImpedimentAsync(id), Times.Once);
        }
    }
}

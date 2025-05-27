using Workflow.Application.Case.Task.DeleteTask;
using Workflow.Application.Case.Task.GetTask;
using Workflow.Application.Case.TaskNotification.NotifyTaskDeleted;
using Application.Service.Task.DeleteTask;
using Workflow.Domain.Case.TaskNotification.NotifyTaskDeleted;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Moq;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Test.Service.TaskTest
{
    public class DeleteTaskServiceTest
    {
        private readonly Mock<IDeleteTaskApplication> _deleteTaskAppMock;
        private readonly Mock<IGetTaskApplication> _getTaskAppMock;
        private readonly Mock<INotifyTaskDeletedApplication> _notifyTaskDeletedAppMock;
        private readonly DeleteTaskService _service;

        public DeleteTaskServiceTest()
        {
            _deleteTaskAppMock = new Mock<IDeleteTaskApplication>();
            _getTaskAppMock = new Mock<IGetTaskApplication>();
            _notifyTaskDeletedAppMock = new Mock<INotifyTaskDeletedApplication>();
            _service = new DeleteTaskService(
                _deleteTaskAppMock.Object,
                _getTaskAppMock.Object,
                _notifyTaskDeletedAppMock.Object
            );
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenTaskNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _getTaskAppMock.Setup(x => x.Execute(id)).ReturnsAsync((ResultDetail<TaskDomain>)null);

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task not found", result.Message);
            _deleteTaskAppMock.Verify(x => x.Execute(It.IsAny<Guid>()), Times.Never);
            _notifyTaskDeletedAppMock.Verify(x => x.Execute(It.IsAny<NotifyTaskDeletedDomain>()), Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenDeleteFails()
        {
            // Arrange
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Description = "desc", Status = EnumTaskStatus.New };
            _getTaskAppMock.Setup(x => x.Execute(id)).ReturnsAsync(task.GetResultDetailSuccess());
            _deleteTaskAppMock.Setup(x => x.Execute(id)).ReturnsAsync(ResultDetailExtensions.GetError<bool>("Delete failed"));

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Delete failed", result.Message);
            _notifyTaskDeletedAppMock.Verify(x => x.Execute(It.IsAny<NotifyTaskDeletedDomain>()), Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldNotNotify_WhenTaskStatusIsNew()
        {
            // Arrange
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Description = "desc", Status = EnumTaskStatus.New };
            _getTaskAppMock.Setup(x => x.Execute(id)).ReturnsAsync(task.GetResultDetailSuccess());
            _deleteTaskAppMock.Setup(x => x.Execute(id)).ReturnsAsync(true.GetResultDetailSuccess());

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.ResultData);
            _notifyTaskDeletedAppMock.Verify(x => x.Execute(It.IsAny<NotifyTaskDeletedDomain>()), Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldNotify_WhenTaskStatusIsNotNew()
        {
            // Arrange
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Description = "desc", Status = EnumTaskStatus.Done };
            _getTaskAppMock.Setup(x => x.Execute(id)).ReturnsAsync(task.GetResultDetailSuccess());
            _deleteTaskAppMock.Setup(x => x.Execute(id)).ReturnsAsync(true.GetResultDetailSuccess());
            _notifyTaskDeletedAppMock.Setup(x => x.Execute(It.IsAny<NotifyTaskDeletedDomain>()))
                .ReturnsAsync(true.GetResultDetailSuccess());

            // Act
            var result = await _service.Execute(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.ResultData);
            _notifyTaskDeletedAppMock.Verify(x => x.Execute(It.Is<NotifyTaskDeletedDomain>(
                n => n.Id == id && n.Description == "desc")), Times.Once);
        }
    }
}

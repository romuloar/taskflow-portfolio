using Workflow.Application.Case.Task.GetTaskById;
using Workflow.Domain.Case.Task.GetTaskById;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Moq;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskTest
{
    public class GetTaskByIdApplicationTest
    {
        private readonly Mock<IGetTaskByIdProvider> _providerMock;
        private readonly GetTaskByIdApplication _application;

        public GetTaskByIdApplicationTest()
        {
            _providerMock = new Mock<IGetTaskByIdProvider>();
            _application = new GetTaskByIdApplication(_providerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenIdIsEmpty()
        {
            var result = await _application.Execute(Guid.Empty);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid Id", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenTaskNotFound()
        {
            var id = Guid.NewGuid();
            _providerMock.Setup(p => p.GetTaskByIdAsync(id)).ReturnsAsync((ResultDetail<TaskDomain>)null);

            var result = await _application.Execute(id);

            Assert.False(result.IsSuccess);
            Assert.Equal("Task not found", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnTask_WhenTaskExists()
        {
            var id = Guid.NewGuid();
            var task = new TaskDomain { Id = id, Description = "Test", Status = EnumTaskStatus.New };
            var expectedResult = task.GetResultDetailSuccess();

            _providerMock.Setup(p => p.GetTaskByIdAsync(id)).ReturnsAsync(expectedResult);

            var result = await _application.Execute(id);

            Assert.True(result.IsSuccess);
            Assert.Equal(task, result.ResultData);
        }
    }
}

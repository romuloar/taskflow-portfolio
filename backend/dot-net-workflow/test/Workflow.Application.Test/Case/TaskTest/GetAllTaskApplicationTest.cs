using Moq;
using Rom.Result.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Application.Case.Task.GetAllTask;
using Workflow.Domain.Case.Task.GetAllTask;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;

namespace Workflow.Application.Test.Case.TaskTest
{
    public class GetAllTaskApplicationTest
    {
        [Fact]
        public async Task ExecuteAsync_ReturnsAllTasks()
        {
            // Arrange
            var mockProvider = new Mock<IGetAllTaskProvider>();
            var tasks = new List<TaskDomain>
                {
                    new TaskDomain { Id = Guid.NewGuid(), Description = "Task 1", Status = EnumTaskStatus.New },
                    new TaskDomain { Id = Guid.NewGuid(), Description = "Task 2", Status = EnumTaskStatus.Done }
                };

            // Fix: Ensure the mocked method returns a ResultDetail<List<TaskDomain>> object
            var resultDetail = await tasks.GetResultDetailSuccessAsync();

            mockProvider.Setup(p => p.GetAllListTaskAsync()).ReturnsAsync(resultDetail);

            var app = new GetAllTaskApplication(mockProvider.Object);

            // Act
            var result = await app.ExecuteAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.ResultData.Count);
        }
    }
}

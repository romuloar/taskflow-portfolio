using Microsoft.EntityFrameworkCore;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Provider.Task;

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Test.Provider.TaskTest
{
    public class GetAllTaskProviderTest
    {
        [Fact]
        public async Task GetAllListTaskAsync_ReturnsAllTasks()
        {
            var options = new DbContextOptionsBuilder<WorkflowDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new WorkflowDbContext(options))
            {
                context.Tasks.AddRange(new List<TaskDomain>
            {
                new TaskDomain { Id = Guid.NewGuid(), Description = "Task 1", Status = EnumTaskStatus.New },
                new TaskDomain { Id = Guid.NewGuid(), Description = "Task 2", Status = EnumTaskStatus.Done }
            });
                context.SaveChanges();

                var provider = new GetAllTaskProvider(context);

                // Act
                var result = await provider.GetAllListTaskAsync();

                // Assert
                Assert.Equal(2, result.ResultData.Count);
            }
        }
    }
}

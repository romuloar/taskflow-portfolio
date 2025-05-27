using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Infra.Adapter.Data.EntityFrameworkCore.Provider.Task;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapter.Data.EntityFrameworkCore.Test.Provider.TaskTest
{
    public class ChangeStatusToInProgressProviderTest
    {
        /// <summary>
        /// Creates a new instance of ChangeStatusToInProgressProvider with the specified context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private ChangeStatusToInProgressProvider CreateProvider(WorkflowDbContext context)
        {
            return new ChangeStatusToInProgressProvider(context);
        }

        /// <summary>
        /// Creates a new instance of WorkflowDbContext using an in-memory database for testing purposes.
        /// </summary>
        /// <returns></returns>
        private WorkflowDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<WorkflowDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new WorkflowDbContext(options);
        }

        /// <summary>
        /// Tests the ChangeStatusToInProgressAsync method to ensure it changes the task status to InProgress and returns a success result.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ChangeStatusToInProgressAsync_Should_Change_Status_And_Return_Success_Result()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Task to be in progress",
                Status = EnumTaskStatus.New
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var result = await provider.ChangeStatusToInProgressAsync(task.Id);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ResultData);
            Assert.Equal(EnumTaskStatus.InProgress, result.ResultData.Status);

            // Verify that the status was updated in the database
            var persisted = await context.Tasks.FindAsync(task.Id);
            Assert.NotNull(persisted);
            Assert.Equal(EnumTaskStatus.InProgress, persisted.Status);
        }

        /// <summary>
        /// Tests the ChangeStatusToInProgressAsync method to ensure it returns an error when the task does not exist.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ChangeStatusToInProgressAsync_Should_Return_NotFound_When_Task_Does_Not_Exist()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var result = await provider.ChangeStatusToInProgressAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.Null(result.ResultData);
        }
    }
}

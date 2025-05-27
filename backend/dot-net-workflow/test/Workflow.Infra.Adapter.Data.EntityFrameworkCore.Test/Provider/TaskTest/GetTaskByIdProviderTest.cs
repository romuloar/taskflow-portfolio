using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Provider.Task;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapter.Data.EntityFrameworkCore.Test.Provider.TaskTest
{
    public class GetTaskByIdProviderTest
    {
        private GetTaskByIdProvider CreateProvider(WorkflowDbContext context)
        {
            return new GetTaskByIdProvider(context);
        }

        /// <summary>
        /// Creates a new instance of WorkflowDbContext using an in-memory database.
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
        /// Tests that GetTaskByIdAsync returns a task when it exists in the database.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetTaskByIdAsync_Should_Return_Task_When_Exists()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Existing Task",
                Status = EnumTaskStatus.New
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var result = await provider.GetTaskByIdAsync(task.Id);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ResultData);
            Assert.Equal(task.Id, result.ResultData.Id);
            Assert.Equal("Existing Task", result.ResultData.Description);
        }

        /// <summary>
        /// Tests that GetTaskByIdAsync returns an error when the task does not exist in the database.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetTaskByIdAsync_Should_Return_Error_When_Task_Does_Not_Exist()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var result = await provider.GetTaskByIdAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.Null(result.ResultData);
            Assert.Equal("Task not found", result.Message); // Ensure the error message matches your implementation
        }
    }
}

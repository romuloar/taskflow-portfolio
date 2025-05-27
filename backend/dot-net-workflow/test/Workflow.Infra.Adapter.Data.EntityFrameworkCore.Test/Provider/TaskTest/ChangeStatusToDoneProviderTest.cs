using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Infra.Adapter.Data.EntityFrameworkCore.Provider.Task;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Adapter.Data.EntityFrameworkCore.Test.Provider.TaskTest
{
    public class ChangeStatusToDoneProviderTest
    {
        /// <summary>
        /// Creates a new instance of the ChangeStatusToDoneProvider for testing purposes.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private ChangeStatusToDoneProvider CreateProvider(WorkflowDbContext context)
        {
            return new ChangeStatusToDoneProvider(context);
        }

        /// <summary>
        /// Creates a new instance of the WorkflowDbContext for testing purposes.
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
        /// Test to ensure that changing the status of a task to "Done" works correctly.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ChangeStatusToDoneAsync_Should_Change_Status_And_Return_Success_Result()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Task to be done",
                Status = EnumTaskStatus.InProgress
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var result = await provider.ChangeStatusToDoneAsync(task.Id);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ResultData);
            Assert.Equal(EnumTaskStatus.Done, result.ResultData.Status);

            // Verify that the status was updated in the database
            var persisted = await context.Tasks.FindAsync(task.Id);
            Assert.NotNull(persisted);
            Assert.Equal(EnumTaskStatus.Done, persisted.Status);
        }

        /// <summary>
        /// Test to ensure that changing the status of a non-existent task returns an error.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ChangeStatusToDoneAsync_Should_Return_NotFound_When_Task_Does_Not_Exist()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var result = await provider.ChangeStatusToDoneAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.Null(result.ResultData);
        }
    }
}

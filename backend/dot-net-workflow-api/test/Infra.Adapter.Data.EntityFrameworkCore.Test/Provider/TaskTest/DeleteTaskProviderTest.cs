using Domain.Entities.Task;
using Domain.Generic.Task;
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
    public class DeleteTaskProviderTest
    {
        /// <summary>
        /// Creates a new instance of the DeleteTaskProvider with the specified context. 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private DeleteTaskProvider CreateProvider(WorkflowDbContext context)
        {
            return new DeleteTaskProvider(context);
        }

        /// <summary>
        /// Creates a new instance of the WorkflowDbContext using an in-memory database.
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
        /// Deletes a task and verifies that it has been removed from the database.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteTaskAsync_Should_Remove_Task_And_Return_Success_Result()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Task to be deleted",
                Status = EnumTaskStatus.New
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var result = await provider.DeleteTaskAsync(task.Id);

            Assert.True(result.IsSuccess);
            Assert.True(result.ResultData);

            // Verify that the task was deleted
            var deleted = await context.Tasks.FindAsync(task.Id);
            Assert.Null(deleted);
        }

        /// <summary>
        /// Attempts to delete a task that does not exist and verifies that the result indicates failure.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteTaskAsync_Should_Return_NotFound_When_Task_Does_Not_Exist()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var result = await provider.DeleteTaskAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.False(result.ResultData);
        }
    }
}

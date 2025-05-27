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
    public class ChangeStatusToImpedimentProviderTest
    {
        /// <summary>
        /// Creates a new instance of ChangeStatusToImpedimentProvider with the specified context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private ChangeStatusToImpedimentProvider CreateProvider(WorkflowDbContext context)
        {
            return new ChangeStatusToImpedimentProvider(context);
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
        /// Tests that the ChangeStatusToImpedimentAsync method changes the status of a task to Impediment and returns a success result.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ChangeStatusToImpedimentAsync_Should_Change_Status_And_Return_Success_Result()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Task to be impeded",
                Status = EnumTaskStatus.InProgress
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var result = await provider.ChangeStatusToImpedimentAsync(task.Id);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ResultData);
            Assert.Equal(EnumTaskStatus.Impediment, result.ResultData.Status);

            // Verify that the status was changed in the database
            var persisted = await context.Tasks.FindAsync(task.Id);
            Assert.NotNull(persisted);
            Assert.Equal(EnumTaskStatus.Impediment, persisted.Status);
        }

        /// <summary>
        /// Tests that the ChangeStatusToImpedimentAsync method returns a failure result when the task does not exist.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ChangeStatusToImpedimentAsync_Should_Return_NotFound_When_Task_Does_Not_Exist()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var result = await provider.ChangeStatusToImpedimentAsync(Guid.NewGuid());

            Assert.False(result.IsSuccess);
            Assert.Null(result.ResultData);
        }
    }
}

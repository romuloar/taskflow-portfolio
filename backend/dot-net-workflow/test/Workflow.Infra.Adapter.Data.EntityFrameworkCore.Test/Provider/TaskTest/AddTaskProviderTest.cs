using Workflow.Domain.Case.Task.AddTask;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Infra.Adapter.Data.EntityFrameworkCore.Provider.Task;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapter.Data.EntityFrameworkCore.Test.Provider.TaskTest
{
    public class AddTaskProviderTest
    {
        /// <summary>
        /// Creates a new instance of the AddTaskProvider with the specified context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private AddTaskProvider CreateProvider(WorkflowDbContext context)
        {
            return new AddTaskProvider(context);
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
        /// Test to ensure that a task can be added successfully and returns the correct result.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddTaskAsync_Should_Add_Task_And_Return_Success_Result()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var task = new AddTaskDomain
            {                
                Description = "AddTask Test",                
            };

            var result = await provider.AddTaskAsync(task);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ResultData);
            Assert.Equal("AddTask Test", result.ResultData.Description);

            // Verifica se foi persistido no banco
            var persisted = await context.Tasks.FindAsync(result.ResultData.Id);
            Assert.NotNull(persisted);
            Assert.Equal("AddTask Test", persisted.Description);
        }

        /// <summary>
        /// Test to ensure that an error during task addition returns an exception result.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddTaskAsync_Should_Return_Exception_Result_On_Error()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            // Pass null to simulate an error
            var result = await provider.AddTaskAsync(null);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ResultData);
        }
    }
}

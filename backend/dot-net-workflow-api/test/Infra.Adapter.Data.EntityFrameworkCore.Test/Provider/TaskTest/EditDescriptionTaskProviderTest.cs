using Domain.Case.Task.EditDescriptionTask;
using Domain.Entities.Task;
using Domain.Generic.Task;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Infra.Adapter.Data.EntityFrameworkCore.Provider.Task;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapter.Data.EntityFrameworkCore.Test.Provider.TaskTest
{
    public class EditDescriptionTaskProviderTest
    {
        /// <summary>
        /// Creates a new instance of EditDescriptionTaskProvider with the given context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private EditDescriptionTaskProvider CreateProvider(WorkflowDbContext context)
        {
            return new EditDescriptionTaskProvider(context);
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
        /// Tests that the EditDescriptionTaskProvider updates the description of an existing task and returns a success result.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EditDescriptionAsync_Should_Update_Description_And_Return_Success_Result()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Old Description",
                Status = EnumTaskStatus.New
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var editTask = new EditDescriptionTaskDomain
            {
                Id = task.Id,
                Description = "New Description"
            };
            
            var result = await provider.EditDescriptionTaskAsync(editTask);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ResultData);
            Assert.Equal(editTask.Description, result.ResultData.Description);

            // Persisted task should have the updated description
            var persisted = await context.Tasks.FindAsync(task.Id);
            Assert.NotNull(persisted);
            Assert.Equal(editTask.Description, persisted.Description);
        }

        /// <summary>
        /// Tests that the EditDescriptionTaskProvider returns an error when trying to edit a task that does not exist.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EditDescriptionAsync_Should_Return_NotFound_When_Task_Does_Not_Exist()
        {
            using var context = CreateContext();
            var provider = CreateProvider(context);
            var editTask = new EditDescriptionTaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Any Description"
            };

            var result = await provider.EditDescriptionTaskAsync(editTask);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ResultData);
        }
    }
}

using Domain.Entities.Task;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Infra.Adapter.Data.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapter.Data.EntityFrameworkCore.Test.Repository
{
    public class BaseRepositoryTest
    {
        private WorkflowDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<WorkflowDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Isola cada teste
                .Options;
            return new WorkflowDbContext(options);
        }

        private BaseRepository<TaskDomain> CreateRepository(WorkflowDbContext context)
        {
            return new BaseRepository<TaskDomain>(context);
        }

        [Fact]
        public async Task AddAsync_Should_Add_Entity()
        {
            using var context = CreateContext();
            var repo = CreateRepository(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Test Add",
                Status = Domain.Generic.Task.EnumTaskStatus.New
            };

            await repo.AddAsync(task);
            await context.SaveChangesAsync();

            var found = await context.Tasks.FindAsync(task.Id);
            Assert.NotNull(found);
            Assert.Equal("Test Add", found.Description);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Entity()
        {
            using var context = CreateContext();
            var repo = CreateRepository(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Test Get",
                Status = Domain.Generic.Task.EnumTaskStatus.New
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var found = await repo.GetByIdAsync(task.Id);
            Assert.NotNull(found);
            Assert.Equal("Test Get", found.Description);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Entities()
        {
            using var context = CreateContext();
            var repo = CreateRepository(context);

            var tasks = new List<TaskDomain>
            {
                new TaskDomain { Id = Guid.NewGuid(), Description = "Task 1", Status = Domain.Generic.Task.EnumTaskStatus.New },
                new TaskDomain { Id = Guid.NewGuid(), Description = "Task 2", Status = Domain.Generic.Task.EnumTaskStatus.Done }
            };

            context.Tasks.AddRange(tasks);
            await context.SaveChangesAsync();

            var all = await repo.GetAllAsync();
            Assert.Equal(2, all.Count);
        }

        [Fact]
        public async Task Update_Should_Update_Entity()
        {
            using var context = CreateContext();
            var repo = CreateRepository(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Before Update",
                Status = Domain.Generic.Task.EnumTaskStatus.New
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            task.Description = "After Update";
            repo.Update(task);
            await context.SaveChangesAsync();

            var updated = await context.Tasks.FindAsync(task.Id);
            Assert.Equal("After Update", updated.Description);
        }

        [Fact]
        public async Task Remove_Should_Remove_Entity()
        {
            using var context = CreateContext();
            var repo = CreateRepository(context);

            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "To Remove",
                Status = Domain.Generic.Task.EnumTaskStatus.New
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            repo.Remove(task);
            await context.SaveChangesAsync();

            var removed = await context.Tasks.FindAsync(task.Id);
            Assert.Null(removed);
        }
    }
}

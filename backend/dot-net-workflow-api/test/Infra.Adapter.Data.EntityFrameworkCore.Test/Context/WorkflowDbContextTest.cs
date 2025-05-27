using Domain.Entities.Task;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Adapter.Data.EntityFrameworkCore.Test.Context
{
    public class WorkflowDbContextTest
    {
        private WorkflowDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<WorkflowDbContext>()
                .UseInMemoryDatabase("WorkflowDbTest")
                .Options;
            return new WorkflowDbContext(options);
        }

        [Fact]
        public void Should_Instantiate_DbContext_And_DbSet()
        {
            using var context = CreateContext();
            Assert.NotNull(context);
            Assert.NotNull(context.Tasks);
        }

        [Fact]
        public void Should_Add_And_Retrieve_TaskDomain()
        {
            using var context = CreateContext();
            var task = new TaskDomain
            {
                Id = Guid.NewGuid(),
                Description = "Test Task",
                Status = Domain.Generic.Task.EnumTaskStatus.New
            };

            context.Tasks.Add(task);
            context.SaveChanges();

            var retrieved = context.Tasks.Find(task.Id);
            Assert.NotNull(retrieved);
            Assert.Equal("Test Task", retrieved.Description);
            Assert.Equal(Domain.Generic.Task.EnumTaskStatus.New, retrieved.Status);
        }

        [Fact]
        public void Should_Apply_Configurations_From_Assembly()
        {
            using var context = CreateContext();
            var entityType = context.Model.FindEntityType(typeof(TaskDomain));
            Assert.Equal("Task", entityType.GetTableName());
            Assert.Equal("tsk", entityType.GetSchema());
        }
    }
}

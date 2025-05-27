using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Config;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapter.Data.EntityFrameworkCore.Test.Config
{
    public class TaskDomainConfigurationTest
    {
        private class TestDbContext : DbContext
        {
            public DbSet<TaskDomain> Tasks { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new TaskDomainConfiguration());
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("TestDb"); // This requires the Microsoft.EntityFrameworkCore.InMemory package
            }
        }

        [Fact]
        public void Should_Configure_Table_And_Properties_Correctly()
        {
            using var context = new TestDbContext();
            var entityType = context.Model.FindEntityType(typeof(TaskDomain)); // Fixed: Use context.Model.FindEntityType instead of context.Tasks.FindEntityType

            Assert.Equal("Task", entityType.GetTableName());
            Assert.Equal("tsk", entityType.GetSchema());
            Assert.Equal("Id", entityType.FindPrimaryKey().Properties[0].Name);

            var description = entityType.FindProperty(nameof(TaskDomain.Description));
            Assert.True(description.IsNullable == false);
            Assert.Equal(50, description.GetMaxLength());

            var status = entityType.FindProperty(nameof(TaskDomain.Status));
            Assert.True(status.IsNullable == false);            
        }

        [Theory]
        [InlineData(EnumTaskStatus.New, "NEW")]
        [InlineData(EnumTaskStatus.InProgress, "IPR")]
        [InlineData(EnumTaskStatus.Impediment, "IMP")]
        [InlineData(EnumTaskStatus.Done, "DON")]
        public void Should_Convert_Enum_To_String(EnumTaskStatus status, string expected)
        {
            using var context = new TestDbContext();
            var converter = context.Model
                .FindEntityType(typeof(TaskDomain)) // Fixed: Use context.Model.FindEntityType instead of context.Tasks.FindEntityType
                .FindProperty(nameof(TaskDomain.Status))
                .GetValueConverter();

            var converted = converter.ConvertToProvider.Invoke(status);
            Assert.Equal(expected, converted);
        }
    }
}

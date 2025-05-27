using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context
{
    public class WorkflowDbContextFactory : IDesignTimeDbContextFactory<WorkflowDbContext>
    {
        public WorkflowDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorkflowDbContext>();
            // Ajuste a string de conexão conforme seu appsettings ou ambiente
            optionsBuilder.UseSqlite("Data Source=workflow.db");

            return new WorkflowDbContext(optionsBuilder.Options);
        }
    }
}

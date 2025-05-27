using Workflow.Domain.Entities.Task;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Config;

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context
{
    public class WorkflowDbContext : DbContext
    {
        public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskDomain> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkflowDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new TaskDomainConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

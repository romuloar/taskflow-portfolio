using Workflow.Domain.Entities.Task;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Workflow.Domain.Generic.Task;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Config
{
    public class TaskDomainConfiguration : IEntityTypeConfiguration<TaskDomain>
    {
        public void Configure(EntityTypeBuilder<TaskDomain> builder)
        {
            builder.ToTable("Task");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            // Define the conversion for EnumTaskStatus to string
            var statusConverter = new ValueConverter<EnumTaskStatus, string>(
                v => v == EnumTaskStatus.New ? "NEW" :
                     v == EnumTaskStatus.InProgress ? "IPR" :
                     v == EnumTaskStatus.Impediment ? "IMP" :
                     v == EnumTaskStatus.Done ? "DNE" :
                     "NEW",
                v => v == "NEW" ? EnumTaskStatus.New :
                     v == "IPR" ? EnumTaskStatus.InProgress :
                     v == "IMP" ? EnumTaskStatus.Impediment :
                     v == "DNE" ? EnumTaskStatus.Done :
                     EnumTaskStatus.New
            );

            builder.Property(t => t.Status)
                .HasConversion(statusConverter)
                .HasColumnType("char(3)")
                .IsRequired();
        }
    }
}

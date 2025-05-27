using Workflow.Domain.Case.Task.ChangeStatusToImpediment;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Infra.Adapter.Data.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Adapter.Data.EntityFrameworkCore.Provider.Task
{
    public class ChangeStatusToImpedimentProvider : BaseRepository<TaskDomain>, IChangeStatusToImpedimentProvider
    {
        public ChangeStatusToImpedimentProvider(WorkflowDbContext context) : base(context) { }

        public async Task<ResultDetail<TaskDomain>> ChangeStatusToImpedimentAsync(Guid id)
        {
            try
            {
                var entity = await base.GetByIdAsync(id);
                if (entity == null)
                {
                    return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task not found");
                }

                entity.Status = EnumTaskStatus.Impediment;
                base.Update(entity);
                await _context.SaveChangesAsync();
                return await entity.GetResultDetailSuccessAsync();
            }
            catch (Exception exc)
            {
                return await exc.GetResultDetailExceptionAsync<TaskDomain>();
            }
        }
    }
}

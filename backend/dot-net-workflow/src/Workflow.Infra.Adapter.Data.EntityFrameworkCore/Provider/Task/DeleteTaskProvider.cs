using Workflow.Domain.Case.Task.DeleteTask;
using Workflow.Domain.Entities.Task;
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
    public class DeleteTaskProvider : BaseRepository<TaskDomain>, IDeleteTaskProvider
    {
        public DeleteTaskProvider(WorkflowDbContext context) : base(context) { }

        public async Task<ResultDetail<bool>> DeleteTaskAsync(Guid id)
        {
            try
            {
                var entity = await base.GetByIdAsync(id);
                if (entity == null)
                {
                    return await false.GetResultDetailErrorAsync("Task not found");
                }

                base.Remove(entity);
                await _context.SaveChangesAsync();
                return await true.GetResultDetailSuccessAsync();
            }
            catch (Exception exc)
            {
                return await exc.GetResultDetailExceptionAsync<bool>();
            }
        }
    }
}

using Workflow.Domain.Case.Task.UpdateTask;
using Workflow.Domain.Entities.Task;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Repository;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Provider.Task
{
    public class UpdateTaskProvider : BaseRepository<TaskDomain>, IUpdateTaskProvider
    {
        public UpdateTaskProvider(WorkflowDbContext context) : base(context) { }        

        public async Task<ResultDetail<TaskDomain>> UpdateTaskAsync(UpdateTaskDomain param)
        {
            try
            {
                var entity = await base.GetByIdAsync(param.Id);
                if (entity == null)
                {
                    return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task not found");
                }

                entity.Description = param.Description;
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

using Workflow.Domain.Case.Task.AddTask;
using Workflow.Domain.Entities.Task;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Infra.Adapter.Data.EntityFrameworkCore.Repository;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Infra.Adapter.Data.EntityFrameworkCore.Provider.Task
{
    public class AddTaskProvider : BaseRepository<TaskDomain>, IAddTaskProvider
    {
        public AddTaskProvider(WorkflowDbContext context) : base(context) { }

        public async Task<ResultDetail<TaskDomain>> AddTaskAsync(AddTaskDomain param)
        {
            
            try
            {
                var entity = new TaskDomain();
                //Todo: CopyProperties method need to be documented 
                // Copy properties from the parameter to the entity when fields match
                entity.Description = param.Description; 
                await base.AddAsync(entity);
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

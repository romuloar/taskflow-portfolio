using Workflow.Domain.Case.Task.GetTaskById;
using Workflow.Domain.Entities.Task;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Repository;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Provider.Task
{
    public class GetTaskByIdProvider : BaseRepository<TaskDomain>, IGetTaskByIdProvider
    {
        public GetTaskByIdProvider(WorkflowDbContext context) : base(context) { }        

        public async Task<ResultDetail<TaskDomain>> GetTaskByIdAsync(Guid id)
        {
            try
            {
                var entity = await base.GetByIdAsync(id);
                if(entity == null)
                {
                    return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task not found");
                }
                return await entity.GetResultDetailSuccessAsync();
            }
            catch (Exception exc)
            {
                return await exc.GetResultDetailExceptionAsync<TaskDomain>();
            }            
        }
    }
}

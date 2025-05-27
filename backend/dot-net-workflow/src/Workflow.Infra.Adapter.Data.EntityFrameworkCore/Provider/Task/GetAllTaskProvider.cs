using Microsoft.EntityFrameworkCore;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using Workflow.Domain.Case.Task.GetAllTask;
using Workflow.Domain.Entities.Task;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context;

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Provider.Task
{
    public class GetAllTaskProvider : IGetAllTaskProvider
    {
        private readonly WorkflowDbContext _context;

        public GetAllTaskProvider(WorkflowDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDetail<List<TaskDomain>>> GetAllListTaskAsync()
        {
            try
            {
                var result = await _context.Tasks.ToListAsync();
                return await result.GetResultDetailSuccessAsync(); // Ensure to return a ResultDetail object
            }
            catch (Exception exc)
            {
                return await exc.GetResultDetailExceptionAsync<List<TaskDomain>>(); // Handle exceptions and return a ResultDetail with error information
            }            
        }
    }
}

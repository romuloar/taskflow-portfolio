using Rom.Result.Domain;
using Workflow.Domain.Case.Task.GetAllTask;
using Workflow.Domain.Entities.Task;

namespace Workflow.Application.Case.Task.GetAllTask
{
    public class GetAllTaskApplication : IGetAllTaskApplication
    {
        private readonly IGetAllTaskProvider _provider;

        public GetAllTaskApplication(IGetAllTaskProvider provider)
        {
            _provider = provider;
        }

        public async Task<ResultDetail<List<TaskDomain>>> ExecuteAsync()
        {
            return await _provider.GetAllListTaskAsync();            
        }
    }
}

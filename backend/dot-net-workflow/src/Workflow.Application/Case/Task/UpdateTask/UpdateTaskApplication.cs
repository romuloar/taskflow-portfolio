using Workflow.Domain.Case.Task.UpdateTask;
using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Workflow.Application.Case.Task.EditDescriptionTask.UpdateTask
{
    public class UpdateTaskApplication : IUpdateTaskApplication
    {
        private readonly IUpdateTaskProvider _provider;
        public UpdateTaskApplication(IUpdateTaskProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<ResultDetail<TaskDomain>> Execute(UpdateTaskDomain param)
        {       
            if (param == null || !param.IsValidDomain)
            {
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Invalid param");
            }
    
            return await _provider.UpdateTaskAsync(param);
        }
    }
}

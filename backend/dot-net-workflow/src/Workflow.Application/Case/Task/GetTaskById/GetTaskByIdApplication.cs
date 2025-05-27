using Workflow.Domain.Case.Task.GetTaskById;
using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Workflow.Application.Case.Task.GetTaskById
{
    public class GetTaskByIdApplication : IGetTaskByIdApplication
    {
        private readonly IGetTaskByIdProvider _provider;

        public GetTaskByIdApplication(IGetTaskByIdProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<ResultDetail<TaskDomain>> Execute(Guid id)
        {
            if (id == Guid.Empty)
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Invalid Id");

            var result = await _provider.GetTaskByIdAsync(id);
            if (result == null || result.ResultData == null)
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task not found");

            return result;
        }
    }
}

using Workflow.Domain.Case.Task.ChangeStatusToDone;
using Workflow.Domain.Case.Task.GetTaskById;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Workflow.Application.Case.Task.ChangeStatusToDone
{
    public class ChangeStatusToDoneApplication : IChangeStatusToDoneApplication
    {
        private readonly IChangeStatusToDoneProvider _provider;
        private readonly IGetTaskByIdProvider _getTaskById;
        public ChangeStatusToDoneApplication(IChangeStatusToDoneProvider provider, IGetTaskByIdProvider getTaskById)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _getTaskById = getTaskById ?? throw new ArgumentNullException(nameof(getTaskById));
        }

        public async Task<ResultDetail<TaskDomain>> ExecuteAsync(Guid id)
        {
            // Validate the input id
            if (id == Guid.Empty)
            {
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Invalid id");
            }

            // Retrieve the task by id to check its current status
            var task = await _getTaskById.GetTaskByIdAsync(id);
            if (task == null)
            {
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task not found");
            }

            // Check if the task is already done
            if (task.ResultData.Status == EnumTaskStatus.Done)
            {
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task already done");                
            }

            return await _provider.ChangeStatusToDoneAsync(id);
        }
    }
}

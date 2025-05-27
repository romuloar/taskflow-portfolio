using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Domain.Case.Task.ChangeStatusToDone
{
    public interface IChangeStatusToDoneProvider
    {
        Task<ResultDetail<TaskDomain>> ChangeStatusToDoneAsync(Guid id);
    }
}

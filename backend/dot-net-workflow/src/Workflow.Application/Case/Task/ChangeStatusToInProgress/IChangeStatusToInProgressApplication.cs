using Workflow.Domain.Case.Task.ChangeStatusToInProgress;
using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Application.Case.Task.ChangeStatusToInProgress
{
    public interface IChangeStatusToInProgressApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(Guid id);
    }
}

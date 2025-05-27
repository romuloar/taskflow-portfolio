using Workflow.Domain.Case.Task.ChangeStatusToDone;
using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Application.Case.Task.ChangeStatusToDone
{
    public interface IChangeStatusToDoneApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(Guid id);
    }
}

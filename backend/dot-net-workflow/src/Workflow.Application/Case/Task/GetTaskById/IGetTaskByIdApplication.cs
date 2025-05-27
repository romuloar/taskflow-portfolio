using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Application.Case.Task.GetTaskById
{
    public interface IGetTaskByIdApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(Guid id);
    }
}

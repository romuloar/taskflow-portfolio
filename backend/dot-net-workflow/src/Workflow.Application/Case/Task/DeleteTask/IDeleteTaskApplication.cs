using Workflow.Domain.Case.Task.DeleteTask;
using Rom.Result.Domain;

namespace Workflow.Application.Case.Task.DeleteTask
{
    public interface IDeleteTaskApplication
    {
        Task<ResultDetail<bool>> Execute(Guid id);
    }
}

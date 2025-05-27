using Rom.Result.Domain;

namespace Workflow.Domain.Case.Task.DeleteTask
{
    public interface IDeleteTaskProvider
    {
        Task<ResultDetail<bool>> DeleteTaskAsync(Guid id);
    }
}

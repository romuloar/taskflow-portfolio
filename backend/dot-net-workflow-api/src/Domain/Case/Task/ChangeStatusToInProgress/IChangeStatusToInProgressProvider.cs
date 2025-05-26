using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Domain.Case.Task.ChangeStatusToInProgress
{
    public interface IChangeStatusToInProgressProvider
    {
        Task<ResultDetail<TaskDomain>> ChangeStatusToInProgressAsync(Guid id);
    }
}

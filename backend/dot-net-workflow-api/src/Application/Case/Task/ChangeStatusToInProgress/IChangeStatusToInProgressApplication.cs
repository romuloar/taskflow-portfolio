using Domain.Case.Task.ChangeStatusToInProgress;
using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Application.Case.Task.ChangeStatusToInProgress
{
    public interface IChangeStatusToInProgressApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(Guid id);
    }
}

using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Domain.Case.Task.ChangeStatusToDone
{
    public interface IChangeStatusToDoneProvider
    {
        Task<ResultDetail<TaskDomain>> ChangeStatusToDoneAsync(Guid id);
    }
}

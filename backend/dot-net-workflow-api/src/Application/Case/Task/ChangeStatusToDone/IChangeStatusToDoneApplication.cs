using Domain.Case.Task.ChangeStatusToDone;
using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Application.Case.Task.ChangeStatusToDone
{
    public interface IChangeStatusToDoneApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(Guid id);
    }
}

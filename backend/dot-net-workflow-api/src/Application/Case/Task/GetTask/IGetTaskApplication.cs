using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Application.Case.Task.GetTask
{
    public interface IGetTaskApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(Guid id);
    }
}

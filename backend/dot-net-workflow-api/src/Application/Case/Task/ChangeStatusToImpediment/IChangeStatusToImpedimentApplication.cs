using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Application.Case.Task.ChangeStatusToImpediment
{
    public interface IChangeStatusToImpedimentApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(Guid id);
    }
}

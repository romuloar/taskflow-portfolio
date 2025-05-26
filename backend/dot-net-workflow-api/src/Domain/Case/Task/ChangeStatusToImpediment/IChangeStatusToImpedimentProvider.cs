using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Domain.Case.Task.ChangeStatusToImpediment
{
    public interface IChangeStatusToImpedimentProvider
    {
        Task<ResultDetail<TaskDomain>> ChangeStatusToImpedimentAsync(Guid id);
    }
}

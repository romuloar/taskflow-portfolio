using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Domain.Case.Task.ChangeStatusToImpediment
{
    public interface IChangeStatusToImpedimentProvider
    {
        Task<ResultDetail<TaskDomain>> ChangeStatusToImpedimentAsync(Guid id);
    }
}

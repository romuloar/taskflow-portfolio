using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Application.Case.Task.ChangeStatusToImpediment
{
    public interface IChangeStatusToImpedimentApplication
    {
        Task<ResultDetail<TaskDomain>> ExecuteAsync(Guid id);
    }
}

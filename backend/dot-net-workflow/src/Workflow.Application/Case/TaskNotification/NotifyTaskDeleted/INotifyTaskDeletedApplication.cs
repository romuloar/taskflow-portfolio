using Workflow.Domain.Case.TaskNotification.NotifyTaskDeleted;
using Rom.Result.Domain;

namespace Workflow.Application.Case.TaskNotification.NotifyTaskDeleted
{
    public interface INotifyTaskDeletedApplication
    {
        Task<ResultDetail<bool>> Execute(NotifyTaskDeletedDomain notification);
    }
}

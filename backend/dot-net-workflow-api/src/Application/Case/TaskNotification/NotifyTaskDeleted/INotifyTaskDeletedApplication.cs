using Domain.Case.TaskNotification.NotifyTaskDeleted;
using Rom.Result.Domain;

namespace Application.Case.TaskNotification.NotifyTaskDeleted
{
    public interface INotifyTaskDeletedApplication
    {
        Task<ResultDetail<bool>> Execute(NotifyTaskDeletedDomain notification);
    }
}

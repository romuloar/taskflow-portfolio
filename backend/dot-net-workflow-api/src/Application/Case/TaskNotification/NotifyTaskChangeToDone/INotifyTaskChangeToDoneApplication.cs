using Domain.Case.TaskNotification.NotifyTaskChangeToDone;
using Rom.Result.Domain;

namespace Application.Case.TaskNotification.NotifyTaskChangeToDone
{
    public interface INotifyTaskChangeToDoneApplication
    {
        Task<ResultDetail<bool>> Execute(NotifyTaskChangeToDoneDomain notification);
    }
}

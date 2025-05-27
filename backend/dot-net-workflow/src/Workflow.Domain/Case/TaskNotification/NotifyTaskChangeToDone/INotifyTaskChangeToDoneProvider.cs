using Rom.Result.Domain;

namespace Workflow.Domain.Case.TaskNotification.NotifyTaskChangeToDone
{
    public interface INotifyTaskChangeToDoneProvider
    {
        Task<ResultDetail<bool>> NotifyAsync(NotifyTaskChangeToDoneDomain notification);
    }
}

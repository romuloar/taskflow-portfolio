using Rom.Result.Domain;

namespace Domain.Case.TaskNotification.NotifyTaskChangeToDone
{
    public interface INotifyTaskChangeToDoneProvider
    {
        Task<ResultDetail<bool>> NotifyAsync(NotifyTaskChangeToDoneDomain notification);
    }
}

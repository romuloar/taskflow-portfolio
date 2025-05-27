using Rom.Result.Domain;
using Rom.Result.Extensions;
using Workflow.Domain.Case.TaskNotification.NotifyTaskDeleted;

namespace Workflow.Infra.Adapter.Notification.Provider
{
    public class NotifyTaskDeletedProvider : INotifyTaskDeletedProvider
    {
        public async Task<ResultDetail<bool>> NotifyAsync(NotifyTaskDeletedDomain notification)
        {
            return await true.GetResultDetailSuccessAsync();
        }
    }
}

using Rom.Result.Domain;
using Rom.Result.Extensions;
using Workflow.Domain.Case.TaskNotification.NotifyTaskChangeToImpediment;

namespace Workflow.Infra.Adapter.Notification.Provider
{
    public class NotifyTaskChangeToImpedimentProvider : INotifyTaskChangeToImpedimentProvider
    {
        public async Task<ResultDetail<bool>> NotifyAsync(NotifyTaskChangeToImpedimentDomain notification)
        {
            return await true.GetResultDetailSuccessAsync();
        }
    }
}

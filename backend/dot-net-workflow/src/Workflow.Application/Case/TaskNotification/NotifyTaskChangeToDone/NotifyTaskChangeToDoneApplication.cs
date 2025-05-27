using Workflow.Domain.Case.TaskNotification.NotifyTaskChangeToDone;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Workflow.Application.Case.TaskNotification.NotifyTaskChangeToDone
{
    public class NotifyTaskChangeToDoneApplication : INotifyTaskChangeToDoneApplication
    {
        private readonly INotifyTaskChangeToDoneProvider _provider;

        public NotifyTaskChangeToDoneApplication(INotifyTaskChangeToDoneProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<ResultDetail<bool>> Execute(NotifyTaskChangeToDoneDomain notification)
        {
            if (notification == null || !notification.IsValidDomain)
                return await ResultDetailExtensions.GetErrorAsync<bool>("Invalid notification domain");

            return await _provider.NotifyAsync(notification);
        }
    }
}

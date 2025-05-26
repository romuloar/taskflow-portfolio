using Domain.Case.TaskNotification.NotifyTaskChangeToImpediment;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Case.TaskNotification.NotifyTaskChangeToImpediment
{
    public class NotifyTaskChangeToImpedimentApplication : INotifyTaskChangeToImpedimentApplication
    {
        private readonly INotifyTaskChangeToImpedimentProvider _provider;

        public NotifyTaskChangeToImpedimentApplication(INotifyTaskChangeToImpedimentProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<ResultDetail<bool>> Execute(NotifyTaskChangeToImpedimentDomain notification)
        {
            if (notification == null || !notification.IsValidDomain)
                return await ResultDetailExtensions.GetErrorAsync<bool>("Invalid notification domain");

            return await _provider.NotifyAsync(notification);
        }
    }
}

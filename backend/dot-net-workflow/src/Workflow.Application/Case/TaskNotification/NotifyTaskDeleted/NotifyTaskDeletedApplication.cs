using Workflow.Domain.Case.TaskNotification.NotifyTaskDeleted;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Application.Case.TaskNotification.NotifyTaskDeleted
{
    public class NotifyTaskDeletedApplication : INotifyTaskDeletedApplication
    {
        private readonly INotifyTaskDeletedProvider _provider;

        public NotifyTaskDeletedApplication(INotifyTaskDeletedProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<ResultDetail<bool>> Execute(NotifyTaskDeletedDomain notification)
        {
            if (notification == null || !notification.IsValidDomain)
                return await ResultDetailExtensions.GetErrorAsync<bool>("Invalid notification domain");

            return await _provider.NotifyAsync(notification);
        }
    }
}

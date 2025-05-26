using Rom.Result.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Case.TaskNotification.NotifyTaskDeleted
{
    public interface INotifyTaskDeletedProvider
    {
        Task<ResultDetail<bool>> NotifyAsync(NotifyTaskDeletedDomain notification);
    }
}

using Rom.Result.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Case.TaskNotification.NotifyTaskChangeToImpediment
{
    public interface INotifyTaskChangeToImpedimentProvider
    {
        Task<ResultDetail<bool>> NotifyAsync(NotifyTaskChangeToImpedimentDomain notification);
    }
}

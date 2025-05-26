using Domain.Case.TaskNotification.NotifyTaskChangeToImpediment;
using Rom.Result.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Case.TaskNotification.NotifyTaskChangeToImpediment
{
    public interface INotifyTaskChangeToImpedimentApplication
    {
        Task<ResultDetail<bool>> Execute(NotifyTaskChangeToImpedimentDomain notification);
    }
}

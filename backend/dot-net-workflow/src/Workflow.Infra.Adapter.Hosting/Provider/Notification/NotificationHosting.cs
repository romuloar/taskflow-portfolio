using Microsoft.Extensions.DependencyInjection;
using Workflow.Domain.Case.TaskNotification.NotifyTaskChangeToDone;
using Workflow.Infra.Adapter.Notification.Provider;
using Workflow.Domain.Case.TaskNotification.NotifyTaskChangeToImpediment;
using Workflow.Domain.Case.TaskNotification.NotifyTaskDeleted;

namespace Infra.Adapter.Hosting.Provider.EntityFramework
{   
    public static class NotificationHosting
    {
        public static IServiceCollection AddNotificationHosting(this IServiceCollection services)
        {            
            
            services.AddScoped<INotifyTaskChangeToDoneProvider, NotifyTaskChangeToDoneProvider>();
            services.AddScoped<INotifyTaskChangeToImpedimentProvider, NotifyTaskChangeToImpedimentProvider>();
            services.AddScoped<INotifyTaskDeletedProvider, NotifyTaskDeletedProvider>();
            return services;
        }
    }
}

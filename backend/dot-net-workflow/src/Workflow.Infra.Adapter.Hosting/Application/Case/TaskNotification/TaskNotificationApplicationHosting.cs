﻿using Workflow.Application.Case.TaskNotification.NotifyTaskChangeToDone;
using Workflow.Application.Case.TaskNotification.NotifyTaskChangeToImpediment;
using Workflow.Application.Case.TaskNotification.NotifyTaskDeleted;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Adapter.Hosting.Workflow.Application.Case.TaskNotification
{
    public static class TaskNotificationApplicationHosting
    {
        public static IServiceCollection AddTaskNotificationApplication(this IServiceCollection services)
        {
            services.AddScoped<INotifyTaskChangeToDoneApplication, NotifyTaskChangeToDoneApplication>();            
            services.AddScoped<INotifyTaskChangeToImpedimentApplication, NotifyTaskChangeToImpedimentApplication>();
            services.AddScoped<INotifyTaskDeletedApplication, NotifyTaskDeletedApplication>();

            return services;
        }
    }
}

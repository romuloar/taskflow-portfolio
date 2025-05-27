using Infra.Adapter.Hosting.Workflow.Application.Case.Task;
using Infra.Adapter.Hosting.Workflow.Application.Case.TaskNotification;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Adapter.Hosting.Application
{
    public static class WorkflowApplicationHosting
    {
        public static IServiceCollection AddWorkflowApplicationHosting(this IServiceCollection services)
        {
            // Registering Task Application Hosting
            services.AddTaskApplicationHosting();
            services.AddTaskNotificationApplication();

            // Registering Task Service Hosting
            services.AddTaskServiceHosting();            

            return services;
        }
    }
}

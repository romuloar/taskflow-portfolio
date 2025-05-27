using Application.Service.Task.DeleteTask;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Adapter.Hosting.Workflow.Application.Case.Task
{
    public static class TaskServiceHosting
    {
        public static IServiceCollection AddTaskServiceHosting(this IServiceCollection services)
        {
            services.AddScoped<IDeleteTaskService, DeleteTaskService>();            
            
            return services;
        }
    }
}

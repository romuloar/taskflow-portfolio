using Infra.Adapter.Hosting.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Adapter.Hosting
{
    public static class WorkflowHosting
    {
        public static IServiceCollection AddWorkflowHosting(this IServiceCollection services)
        {
            // Registering Application Hosting
            services.AddWorkflowApplicationHosting();
           
            return services;
        }
    }
}

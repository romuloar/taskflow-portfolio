using Infra.Adapter.Hosting.Application;
using Infra.Adapter.Hosting.Provider.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Adapter.Hosting
{
    public static class WorkflowHosting
    {
        public static IServiceCollection AddWorkflowHosting(this IServiceCollection services)
        {
            // Registering Entity Framework Core Hosting and providers
            services.AddEfHosting();

            // Registering notification Providers 
            services.AddNotificationHosting();

            // Registering Application Hosting
            services.AddWorkflowApplicationHosting();

            return services;
        }
    }
}

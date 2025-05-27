using Workflow.Application.Case.Task.AddTask;
using Workflow.Application.Case.Task.ChangeStatusToDone;
using Workflow.Application.Case.Task.ChangeStatusToImpediment;
using Workflow.Application.Case.Task.ChangeStatusToInProgress;
using Workflow.Application.Case.Task.DeleteTask;
using Workflow.Application.Case.Task.EditDescriptionTask;
using Workflow.Application.Case.Task.GetTask;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Adapter.Hosting.Workflow.Application.Case.Task
{
    public static class TaskApplicationHosting
    {
        public static IServiceCollection AddTaskApplicationHosting(this IServiceCollection services)
        {
            services.AddScoped<IAddTaskApplication, AddTaskApplication>();
            services.AddScoped<IChangeStatusToDoneApplication, ChangeStatusToDoneApplication>();
            services.AddScoped<IChangeStatusToImpedimentApplication, ChangeStatusToImpedimentApplication>();
            services.AddScoped<IChangeStatusToInProgressApplication, ChangeStatusToInProgressApplication>();
            services.AddScoped<IDeleteTaskApplication, DeleteTaskApplication>();
            services.AddScoped<IEditDescriptionTaskApplication, EditDescriptionTaskApplication>();
            services.AddScoped<IGetTaskApplication, GetTaskApplication>();
            
            return services;
        }
    }
}

using Application.Case.Task.AddTask;
using Application.Case.Task.ChangeStatusToDone;
using Application.Case.Task.ChangeStatusToImpediment;
using Application.Case.Task.ChangeStatusToInProgress;
using Application.Case.Task.DeleteTask;
using Application.Case.Task.EditDescriptionTask;
using Application.Case.Task.GetTask;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Adapter.Hosting.Application.Case.Task
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

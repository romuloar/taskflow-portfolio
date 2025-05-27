using Workflow.Application.Case.Task.AddTask;
using Workflow.Application.Case.Task.ChangeStatusToDone;
using Workflow.Application.Case.Task.ChangeStatusToImpediment;
using Workflow.Application.Case.Task.ChangeStatusToInProgress;
using Workflow.Application.Case.Task.DeleteTask;
using Workflow.Application.Case.Task.EditDescriptionTask.UpdateTask;
using Workflow.Application.Case.Task.GetTaskById;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Case.Task.GetAllTask;

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
            services.AddScoped<IUpdateTaskApplication, UpdateTaskApplication>();
            services.AddScoped<IGetTaskByIdApplication, GetTaskByIdApplication>();
            services.AddScoped<IGetAllTaskApplication, GetAllTaskApplication>();

            return services;
        }
    }
}

using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Provider.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Domain.Case.Task.AddTask;
using Workflow.Domain.Case.Task.ChangeStatusToDone;
using Workflow.Domain.Case.Task.ChangeStatusToInProgress;
using Workflow.Domain.Case.Task.DeleteTask;
using Workflow.Domain.Case.Task.UpdateTask;
using Workflow.Domain.Case.Task.GetTaskById;
using Workflow.Domain.Case.Task.GetAllTask;
using Workflow.Domain.Case.Task.ChangeStatusToImpediment;

namespace Infra.Adapter.Hosting.Provider.EntityFramework
{   
    public static class EntityFrameworkCoreHosting
    {
        public static IServiceCollection AddEfHosting(this IServiceCollection services)
        {            
            services.AddDbContext<WorkflowDbContext>(options =>
                options.UseSqlite("Data Source=workflow.db"));

            services.AddScoped<IAddTaskProvider, AddTaskProvider>();            
            services.AddScoped<IChangeStatusToDoneProvider, ChangeStatusToDoneProvider>();
            services.AddScoped<IChangeStatusToInProgressProvider, ChangeStatusToInProgressProvider>();
            services.AddScoped<IChangeStatusToImpedimentProvider, ChangeStatusToImpedimentProvider>();
            services.AddScoped<IDeleteTaskProvider, DeleteTaskProvider>();
            services.AddScoped<IUpdateTaskProvider, UpdateTaskProvider>();
            services.AddScoped<IGetTaskByIdProvider, GetTaskByIdProvider>();
            services.AddScoped<IGetAllTaskProvider, GetAllTaskProvider>();

            return services;
        }
    }
}

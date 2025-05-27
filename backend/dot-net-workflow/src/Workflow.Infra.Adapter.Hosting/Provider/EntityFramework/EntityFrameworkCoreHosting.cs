using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Adapter.Hosting.Provider.EntityFramework
{   
    public static class EntityFrameworkCoreHosting
    {
        public static IServiceCollection AddWorkflowHosting(this IServiceCollection services)
        {            
            services.AddDbContext<WorkflowDbContext>(options =>
                options.UseSqlite("Data Source=workflow.db"));
            
            return services;
        }
    }
}

using Domain.Case.Task.EditDescriptionTask;
using Domain.Entities.Task;
using Infra.Adapter.Data.EntityFrameworkCore.Context;
using Infra.Adapter.Data.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Adapter.Data.EntityFrameworkCore.Provider.Task
{
    public class EditDescriptionTaskProvider : BaseRepository<TaskDomain>, IEditDescriptionTaskProvider
    {
        public EditDescriptionTaskProvider(WorkflowDbContext context) : base(context) { }        

        public async Task<ResultDetail<TaskDomain>> EditDescriptionTaskAsync(EditDescriptionTaskDomain param)
        {
            try
            {
                var entity = await base.GetByIdAsync(param.Id);
                if (entity == null)
                {
                    return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task not found");
                }

                entity.Description = param.Description;
                base.Update(entity);
                await _context.SaveChangesAsync();
                return await entity.GetResultDetailSuccessAsync();
            }
            catch (Exception exc)
            {
                return await exc.GetResultDetailExceptionAsync<TaskDomain>();
            }
        }
    }
}

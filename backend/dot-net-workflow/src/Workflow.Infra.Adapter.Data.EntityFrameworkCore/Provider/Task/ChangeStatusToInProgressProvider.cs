﻿using Workflow.Domain.Case.Task.ChangeStatusToInProgress;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Context;
using Workflow.Infra.Adapter.Data.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;
using Rom.Result.Domain;
using Rom.Result.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Infra.Adapter.Data.EntityFrameworkCore.Provider.Task
{
    public class ChangeStatusToInProgressProvider : BaseRepository<TaskDomain>, IChangeStatusToInProgressProvider
    {
        public ChangeStatusToInProgressProvider(WorkflowDbContext context) : base(context) { }

        public async Task<ResultDetail<TaskDomain>> ChangeStatusToInProgressAsync(Guid id)
        {
            try
            {
                var entity = await base.GetByIdAsync(id);
                if (entity == null)
                {
                    return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task not found");
                }                    

                entity.Status = EnumTaskStatus.InProgress;
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

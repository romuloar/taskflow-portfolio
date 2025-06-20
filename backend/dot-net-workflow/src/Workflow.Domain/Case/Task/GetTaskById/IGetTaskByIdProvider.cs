﻿using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Domain.Case.Task.GetTaskById
{
    public interface IGetTaskByIdProvider
    {
        Task<ResultDetail<TaskDomain>> GetTaskByIdAsync(Guid id);
    }
}

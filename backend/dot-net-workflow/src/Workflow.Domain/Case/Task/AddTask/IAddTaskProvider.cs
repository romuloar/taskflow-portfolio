﻿using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Domain.Case.Task.AddTask
{
    public interface IAddTaskProvider
    {
        /// <summary>
        /// Adds a new task asynchronously.
        /// </summary>
        /// <param name="addTask"></param>
        /// <returns></returns>
        Task<ResultDetail<TaskDomain>> AddTaskAsync(AddTaskDomain addTask);
    }
}

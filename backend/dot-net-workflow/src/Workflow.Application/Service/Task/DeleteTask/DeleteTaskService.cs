﻿using Workflow.Application.Case.Task.DeleteTask;
using Workflow.Application.Case.Task.GetTaskById;
using Workflow.Application.Case.TaskNotification.NotifyTaskDeleted;
using Workflow.Domain.Case.TaskNotification.NotifyTaskDeleted;
using Workflow.Domain.Generic.Task;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Service.Task.DeleteTask
{
    public class DeleteTaskService : IDeleteTaskService
    {
        private readonly IDeleteTaskApplication _deleteTaskApplication;
        private readonly IGetTaskByIdApplication _getTaskApplication;
        private readonly INotifyTaskDeletedApplication _notifyTaskDeletedApplication;

        public DeleteTaskService(
            IDeleteTaskApplication deleteTaskApplication,
            IGetTaskByIdApplication getTaskApplication,
            INotifyTaskDeletedApplication notifyTaskDeletedApplication)
        {
            _deleteTaskApplication = deleteTaskApplication ?? throw new ArgumentNullException(nameof(deleteTaskApplication));
            _getTaskApplication = getTaskApplication ?? throw new ArgumentNullException(nameof(getTaskApplication));
            _notifyTaskDeletedApplication = notifyTaskDeletedApplication ?? throw new ArgumentNullException(nameof(notifyTaskDeletedApplication));
        }

        public async Task<ResultDetail<bool>> Execute(Guid id)
        {
            // Get the task to check its status
            var taskResult = await _getTaskApplication.Execute(id);
            if (taskResult == null || !taskResult.IsSuccess || taskResult.ResultData == null)
                return ResultDetailExtensions.GetError<bool>("Task not found");

            // Delete the task
            var deleteResult = await _deleteTaskApplication.ExecuteAsync(id);
            if (!deleteResult.IsSuccess)
                return deleteResult;

            // Notify if status is not New
            if (taskResult.ResultData.Status != EnumTaskStatus.New)
            {
                var notification = new NotifyTaskDeletedDomain
                {
                    Id = taskResult.ResultData.Id,
                    Description = taskResult.ResultData.Description
                };
                await _notifyTaskDeletedApplication.Execute(notification);
            }

            return deleteResult;
        }
    }
}

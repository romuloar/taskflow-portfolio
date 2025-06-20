﻿using Workflow.Domain.Case.Task.ChangeStatusToImpediment;
using Workflow.Domain.Case.Task.GetTaskById;
using Workflow.Domain.Entities.Task;
using Workflow.Domain.Generic.Task;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Workflow.Application.Case.Task.ChangeStatusToImpediment
{
    public class ChangeStatusToImpedimentApplication : IChangeStatusToImpedimentApplication
    {
        private readonly IChangeStatusToImpedimentProvider _provider;
        private readonly IGetTaskByIdProvider _getTaskById;

        public ChangeStatusToImpedimentApplication(
            IChangeStatusToImpedimentProvider provider,
            IGetTaskByIdProvider getTaskById)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _getTaskById = getTaskById ?? throw new ArgumentNullException(nameof(getTaskById));
        }

        public async Task<ResultDetail<TaskDomain>> ExecuteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Invalid id");

            var task = await _getTaskById.GetTaskByIdAsync(id);
            if (task == null)
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task not found");

            if (task.ResultData.Status == EnumTaskStatus.Impediment)
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task already impediment");

            return await _provider.ChangeStatusToImpedimentAsync(id);
        }
    }
}

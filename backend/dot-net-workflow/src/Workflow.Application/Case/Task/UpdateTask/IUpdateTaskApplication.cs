using Workflow.Domain.Case.Task.UpdateTask;
using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Application.Case.Task.EditDescriptionTask.UpdateTask
{
    public interface IUpdateTaskApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(UpdateTaskDomain param);
    }
}

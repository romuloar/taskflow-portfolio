using Workflow.Domain.Case.Task.EditDescriptionTask;
using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Application.Case.Task.EditDescriptionTask
{
    public interface IEditDescriptionTaskApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(EditDescriptionTaskDomain param);
    }
}

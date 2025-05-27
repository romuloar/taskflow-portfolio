using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Domain.Case.Task.EditDescriptionTask
{
    public interface IEditDescriptionTaskProvider
    {
        Task<ResultDetail<TaskDomain>> EditDescriptionTaskAsync(EditDescriptionTaskDomain param);
    }
}

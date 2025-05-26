using Domain.Case.Task.EditDescriptionTask;
using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Application.Case.Task.EditDescriptionTask
{
    public interface IEditDescriptionTaskApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(EditDescriptionTaskDomain param);
    }
}

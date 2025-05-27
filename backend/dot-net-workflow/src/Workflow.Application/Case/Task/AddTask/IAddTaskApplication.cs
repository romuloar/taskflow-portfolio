using Workflow.Domain.Case.Task.AddTask;
using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Application.Case.Task.AddTask
{
    public interface IAddTaskApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(AddTaskDomain addTask);
    }
}

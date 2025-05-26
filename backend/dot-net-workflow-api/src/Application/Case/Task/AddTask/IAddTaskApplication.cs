using Domain.Case.Task.AddTask;
using Domain.Entities.Task;
using Rom.Result.Domain;

namespace Application.Case.Task.AddTask
{
    public interface IAddTaskApplication
    {
        Task<ResultDetail<TaskDomain>> Execute(AddTaskDomain addTask);
    }
}

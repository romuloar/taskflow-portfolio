using Rom.Result.Domain;
using Workflow.Domain.Entities.Task;

namespace Workflow.Application.Case.Task.GetAllTask
{
    public interface IGetAllTaskApplication
    {
        Task<ResultDetail<List<TaskDomain>>> ExecuteAsync();
    }
}

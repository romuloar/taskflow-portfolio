using Rom.Result.Domain;
using Workflow.Domain.Entities.Task;

namespace Workflow.Domain.Case.Task.GetAllTask
{
    public interface IGetAllTaskProvider
    {
        /// <summary>
        /// Retrieves a list of all tasks.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a list of tasks.</returns>
        Task<ResultDetail<List<TaskDomain>>> GetAllListTaskAsync();
    }
}

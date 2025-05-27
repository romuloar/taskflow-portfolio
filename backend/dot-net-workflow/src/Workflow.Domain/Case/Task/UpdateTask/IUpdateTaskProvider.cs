using Workflow.Domain.Entities.Task;
using Rom.Result.Domain;

namespace Workflow.Domain.Case.Task.UpdateTask
{
    /// <summary>    
    /// use case define the contract for updating tasks.
    /// </summary>
    public interface IUpdateTaskProvider
    {
        Task<ResultDetail<TaskDomain>> UpdateTaskAsync(UpdateTaskDomain param);
    }
}

using Domain.Case.Task.AddTask;
using Domain.Entities.Task;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Case.Task.AddTask
{
    public class AddTaskApplication : IAddTaskApplication
    {
        private readonly IAddTaskProvider _provider;
        public AddTaskApplication(IAddTaskProvider addTask) 
        {
            _provider = addTask ?? throw new ArgumentNullException(nameof(addTask));
        }

        public async Task<ResultDetail<TaskDomain>> Execute(AddTaskDomain addTask)
        {
            if (addTask == null)
            {
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Task is required");
            }

            if(!addTask.IsValidDomain)
            {
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Invalid domain");
            }

            return await _provider.AddTaskAsync(addTask);
        }
    }
}

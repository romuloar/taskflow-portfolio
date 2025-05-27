using Rom.Result.Domain;

namespace Application.Service.Task.DeleteTask
{
    public interface IDeleteTaskService
    {
        Task<ResultDetail<bool>> Execute(Guid id);
    }
}

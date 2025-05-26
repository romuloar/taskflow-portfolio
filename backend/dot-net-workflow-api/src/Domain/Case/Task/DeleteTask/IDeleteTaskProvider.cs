using Rom.Result.Domain;

namespace Domain.Case.Task.DeleteTask
{
    public interface IDeleteTaskProvider
    {
        Task<ResultDetail<bool>> DeleteTaskAsync(Guid id);
    }
}

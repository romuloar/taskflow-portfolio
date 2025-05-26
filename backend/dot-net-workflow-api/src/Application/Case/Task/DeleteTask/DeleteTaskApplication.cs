using Domain.Case.Task.DeleteTask;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Case.Task.DeleteTask
{
    public class DeleteTaskApplication : IDeleteTaskApplication
    {
        private readonly IDeleteTaskProvider _provider;
        public DeleteTaskApplication(IDeleteTaskProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<ResultDetail<bool>> Execute(Guid id)
        {
            // Validate the Id
            if (id == Guid.Empty)
                return await ResultDetailExtensions.GetErrorAsync<bool>("Invalid Id");
            
            return await _provider.DeleteTaskAsync(id);
        }
    }
}

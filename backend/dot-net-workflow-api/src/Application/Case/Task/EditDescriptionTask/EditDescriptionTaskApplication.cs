using Domain.Case.Task.EditDescriptionTask;
using Domain.Entities.Task;
using Rom.Result.Domain;
using Rom.Result.Extensions;

namespace Application.Case.Task.EditDescriptionTask
{
    public class EditDescriptionTaskApplication : IEditDescriptionTaskApplication
    {
        private readonly IEditDescriptionTaskProvider _provider;
        public EditDescriptionTaskApplication(IEditDescriptionTaskProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<ResultDetail<TaskDomain>> Execute(EditDescriptionTaskDomain param)
        {       
            if (param == null || !param.IsValidDomain)
            {
                return await ResultDetailExtensions.GetErrorAsync<TaskDomain>("Invalid param");
            }
    
            return await _provider.EditDescriptionTaskAsync(param);
        }
    }
}

using Rom.Result.Domain;
using Workflow.Domain.Case.TaskNotification.NotifyTaskChangeToDone;
using Rom.Result.Extensions;

namespace Workflow.Infra.Adapter.Notification.Provider
{
    public class NotifyTaskChangeToDoneProvider : INotifyTaskChangeToDoneProvider
    {       

        //todo: when notification api is ready, remove this check
        //private readonly HttpClient _httpClient;

        //public NotifyTaskChangeToDoneProvider(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        public async Task<ResultDetail<bool>> NotifyAsync(NotifyTaskChangeToDoneDomain notification)
        {
            return await true.GetResultDetailSuccessAsync();

            //var content = new StringContent(JsonSerializer.Serialize(notification), Encoding.UTF8, "application/json");

            //// Adjust the URL to your gateway endpoint
            //var response = await _httpClient.PostAsync("/api/notify/task-done", content);

            //if (response.IsSuccessStatusCode)
            //    return await true.GetResultDetailSuccessAsync();

            //return await false.GetResultDetailErrorAsync("Failed to notify gateway");
        }        
    }
}

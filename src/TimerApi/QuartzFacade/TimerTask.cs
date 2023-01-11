using Quartz;
namespace TimerApi.QuartzFacade;
public class TimerTask : IJob
{
    private readonly ILogger<TimerTask> _logger;
    public TimerTask(ILogger<TimerTask> logger) => _logger = logger;
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var id = context.Trigger.Key.Name;
            var callbackUrl = context.JobDetail.JobDataMap.GetString("CallbackUrl");
            if (string.IsNullOrEmpty(callbackUrl))
            {
                _logger.LogWarning("Empty callback Url of Timer Id: {Id}", id);
                return;
            }
            using var client = new HttpClient();
            using (var response = await client.GetAsync($"{callbackUrl.TrimEnd('/')}/{id}"))
                _logger.LogInformation("Timer {Id} executed: {Response}", id, 
                    await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync());
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, e.Message);
        }
    }
}

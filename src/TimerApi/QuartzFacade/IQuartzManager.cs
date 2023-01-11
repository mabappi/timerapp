using TimerApi.ApiModels;
namespace TimerApi.QuartzFacade;
public interface IQuartzManager
{
    Task StartAsync();
    Task StopAsync();
    Task<string> SaveTrigger(string id, SetTimerRequest task);
    Task<int> GetNextFireTime(string triggerKey);
}

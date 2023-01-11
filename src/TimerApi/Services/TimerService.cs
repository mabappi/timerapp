using TimerApi.ApiModels;
using TimerApi.QuartzFacade;

namespace TimerApi.Services;
public class TimerService : ITimerService
{
    private readonly IQuartzManager _quartzManager;
    public TimerService(IQuartzManager quartzManager) => _quartzManager = quartzManager;
    public async Task<string> SetTimer(SetTimerRequest request) => await _quartzManager.SaveTrigger(Guid.NewGuid().ToString(), request);
    public async Task<int> GetTimer(string id) => await _quartzManager.GetNextFireTime(id);
}
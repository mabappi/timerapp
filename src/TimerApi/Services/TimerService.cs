using TimerApi.ApiModels;

namespace TimerApi.Services;
public class TimerService : ITimerService
{
    public async Task<string> SetTimer(SetTimerRequest request) => Guid.NewGuid().ToString();

    public async Task<int> GetTimer(string id) => 100;
}
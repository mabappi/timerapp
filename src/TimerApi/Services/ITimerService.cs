using TimerApi.ApiModels;

namespace TimerApi.Services;

public interface ITimerService
{
    Task<int> GetTimer(string id);
    Task<string> SetTimer(SetTimerRequest request);
}
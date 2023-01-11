using Quartz;
using TimerApi.ApiModels;
namespace TimerApi.QuartzFacade;
public class QuartzManager : IQuartzManager
{
    private readonly IScheduler _scheduler;
    public QuartzManager(IScheduler scheduler) => _scheduler = scheduler;
    public async Task<int> GetNextFireTime(string id)
    {
        var result = await _scheduler.GetTrigger(new TriggerKey(id));
        if (result != null)
            return (int)result.GetNextFireTimeUtc().Value.Subtract(DateTime.Now).TotalSeconds;
        
        var job = await _scheduler.GetJobDetail(new JobKey(id));
        return job == null ? -1 : 0;
    }
    public async Task<string> SaveTrigger(string id, SetTimerRequest task) =>
        await _scheduler.ScheduleJob(
            JobBuilder.Create<TimerTask>()
                .PersistJobDataAfterExecution(true)
                .StoreDurably(true)
                .SetJobData(new JobDataMap(new Dictionary<string, string> { { "CallbackUrl", task.CallbackUrl } }))
                .WithIdentity(id).Build(),
            TriggerBuilder.Create()
                .WithIdentity(id)
                .StartAt(DateTime.Now.Add(new TimeSpan(task.Hours, task.Minutes, task.Seconds)))
                .WithSimpleSchedule(x => x.WithMisfireHandlingInstructionIgnoreMisfires())
                .ForJob(id)
                .Build()).ContinueWith((x) => id);
    public async Task StartAsync() => await _scheduler.Start();
    public async Task StopAsync() => await _scheduler.Shutdown();
}

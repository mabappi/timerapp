using Quartz.Spi;
using Quartz;
namespace TimerApi.QuartzFacade;
public class JobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;
    public JobFactory(IServiceProvider container) => _serviceProvider = container;
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        using var serviceScope = _serviceProvider.CreateScope();
        return serviceScope.ServiceProvider.GetService<TimerTask>()!;
    }
    public void ReturnJob(IJob job) => (job as IDisposable)?.Dispose();
}

namespace TimerApi.QuartzFacade;
public class QuartzHostedService : IHostedService
{
    private readonly IQuartzManager _quartzManager;
    public QuartzHostedService(IQuartzManager quartzManager) => _quartzManager = quartzManager;
    public async Task StartAsync(CancellationToken cancellationToken) => await _quartzManager.StartAsync();
    public async Task StopAsync(CancellationToken cancellationToken) => await _quartzManager.StopAsync();
}
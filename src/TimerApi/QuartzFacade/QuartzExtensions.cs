using Quartz.Impl.AdoJobStore.Common;
using Quartz.Impl.AdoJobStore;
using Quartz.Impl;
using Quartz.Simpl;
using Quartz.Util;
namespace TimerApi.QuartzFacade;
public static class QuartzExtensions
{
    public static async Task<IServiceCollection> AddQuartz(this IServiceCollection services, string connectionString)
    {
        var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
        if (loggerFactory != null)
            Quartz.Logging.LogContext.SetCurrentLogProvider(loggerFactory);

        var logger = loggerFactory?.CreateLogger("Quartz");
        services.AddScoped<TimerTask>();
        services.AddSingleton<IQuartzManager, QuartzManager>();
        services.AddHostedService<QuartzHostedService>();

        DirectSchedulerFactory.Instance.CreateScheduler("Timer", "TimerApp", new DefaultThreadPool(),
            SetupJobStore(connectionString));
        var scheduler = await SchedulerRepository.Instance.Lookup("Timer");
        if (scheduler == null)
        {
            logger?.LogCritical("Unable to setup Quartz.");
            return services;
        }

        scheduler.JobFactory = new JobFactory(services.BuildServiceProvider());
        services.AddSingleton(scheduler);
        return services;
    }
    private static JobStoreTX SetupJobStore(string connectionString)
    {
        DBConnectionManager.Instance.AddConnectionProvider("timerDs", new DbProvider("MySql", connectionString));
        var serializer = new JsonObjectSerializer();
        serializer.Initialize();
        return new JobStoreTX
        {
            DataSource = "timerDs",
            TablePrefix = "QRTZ_",
            InstanceId = "TimerApp",
            DriverDelegateType = "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz",
            ObjectSerializer = serializer,
            InstanceName = "TimerApp",
            PerformSchemaValidation = true,
        };
    }
}
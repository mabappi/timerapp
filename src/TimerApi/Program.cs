using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using TimerApi.Middlewares;
using TimerApi.QuartzFacade;
using TimerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());
var connectionString =
builder.Configuration.GetConnectionString(QuartzDbContext.ConnectionString) ?? string.Empty;
builder.Services.AddDbContext<QuartzDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddHealthChecks()
    .AddCheck("Timer Api", () => HealthCheckResult.Healthy())
    .AddMySql(connectionString, "Database");

builder.Services.AddScoped<ITimerService, TimerService>();
builder.Services.AddControllers();
await builder.Services.AddQuartz(connectionString);
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "Timer Api", Version = "v1" }); });

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetService<QuartzDbContext>();
if (context != null)
    await context.Database.MigrateAsync();

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true
    })
    .UseHealthChecks("/healthz", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

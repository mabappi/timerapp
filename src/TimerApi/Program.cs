using Serilog;
using TimerApi.Middlewares;
using TimerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

builder.Services.AddScoped<ITimerService, TimerService>();
builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "Timer Api", Version = "v1" }); });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

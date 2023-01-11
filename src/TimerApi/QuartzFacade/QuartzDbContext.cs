using Microsoft.EntityFrameworkCore;

namespace TimerApi.QuartzFacade;

public class QuartzDbContext : DbContext
{
    internal const string ConnectionString = nameof(ConnectionString);
    private readonly IConfiguration _configuration;

    public QuartzDbContext(DbContextOptions<QuartzDbContext> context,
            IConfiguration configuration) : base(context) => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_configuration.GetConnectionString(ConnectionString),
            ServerVersion.AutoDetect(_configuration.GetConnectionString(ConnectionString)));
    }
}

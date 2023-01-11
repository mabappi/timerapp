using Microsoft.EntityFrameworkCore;

namespace TimerApi.QuartzFacade;
public static class QuartzExtensions
{
    public static async Task<IServiceCollection> AddQuartz(this IServiceCollection services, string connectionString)
    {

        return services;
    }
}

using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;

namespace AcceptanceTests.Support;

internal static class ConfigurationHelper
{
    static IConfiguration _configuration;
    static ConfigurationHelper() => _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    public static string ApiUrl => _configuration["apiUrl"];
    public static string TimersUrl => $"{ApiUrl}timers";
}

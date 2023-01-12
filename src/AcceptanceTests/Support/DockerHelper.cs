using System.Diagnostics;
namespace AcceptanceTests.Support;
internal static class DockerHelper
{
    public static void StartDocker() => ExecuteProcess("up -d");
    public static void StopDocker() => ExecuteProcess("down");
    public static void StopApiService() => ExecuteProcess("stop timerapi");
    public static void StartApiService() => ExecuteProcess("start timerapi");
    private static void ExecuteProcess(string args) => 
        Process.Start(new ProcessStartInfo
        {
            FileName = "docker-compose.exe",
            Arguments = args
        })?.WaitForExit();
}

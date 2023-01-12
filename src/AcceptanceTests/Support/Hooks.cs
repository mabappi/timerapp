namespace AcceptanceTests.Support;

[Binding]
public class Hooks
{
    [BeforeFeature]
    public static void SetupDocker(FeatureContext featureContext)
    {
        DockerHelper.StartDocker();
        Thread.Sleep(TimeSpan.FromSeconds(5));
    }
    [AfterFeature]
    public static void ShutdownDocker(FeatureContext featureContext)
    {
        DockerHelper.StopDocker();
    }
}

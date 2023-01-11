namespace AcceptanceTests.StepDefinitions;

[Binding]
public sealed class TimerStepDefinitions
{
    [Given(@"The Rest Api endpoint is live")]
    public void GivenTheRestApiEndpointIsLive()
    {
        throw new PendingStepException();
    }

    [When(@"Set timer is called with no JSON payload")]
    public void WhenSetTimerIsCalledWithNoJSONPayload()
    {
        throw new PendingStepException();
    }

    [Then(@"Should return Bad Request")]
    public void ThenShouldReturnBadRequest()
    {
        throw new PendingStepException();
    }

    [When(@"Set timer is called with empty Callback url")]
    public void WhenSetTimerIsCalledWithEmptyCallbackUrl()
    {
        throw new PendingStepException();
    }

    [When(@"Set timer is called with (.*) hours (.*) minute and (.*) seconds and callback url ""([^""]*)""")]
    public void WhenSetTimerIsCalledWithHoursMinuteAndSecondsAndCallbackUrl(int p0, int p1, int p2, string p3)
    {
        throw new PendingStepException();
    }

    [Then(@"Should return an Id")]
    public void ThenShouldReturnAnId()
    {
        throw new PendingStepException();
    }

    [Then(@"the url should be called after (.*) minute")]
    public void ThenTheUrlShouldBeCalledAfterMinute(int p0)
    {
        throw new PendingStepException();
    }

    [Then(@"the url should be called immetiately")]
    public void ThenTheUrlShouldBeCalledImmetiately()
    {
        throw new PendingStepException();
    }

    [When(@"Get timer is call with empty Id")]
    public void WhenGetTimerIsCallWithEmptyId()
    {
        throw new PendingStepException();
    }

    [When(@"Get timer is call with a not known Id")]
    public void WhenGetTimerIsCallWithANotKnownId()
    {
        throw new PendingStepException();
    }

    [Then(@"Should return Not Found")]
    public void ThenShouldReturnNotFound()
    {
        throw new PendingStepException();
    }

    [Given(@"Set timer is call with (.*) hours (.*) minute and (.*) seconds and callback url ""([^""]*)""")]
    public void GivenSetTimerIsCallWithHoursMinuteAndSecondsAndCallbackUrl(int p0, int p1, int p2, string p3)
    {
        throw new PendingStepException();
    }

    [When(@"Get tiemer status API is called using the Id")]
    public void WhenGetTiemerStatusAPIIsCalledUsingTheId()
    {
        throw new PendingStepException();
    }

    [Then(@"Should return JSON data with Id and the time left should be less then (.*) minute")]
    public void ThenShouldReturnJSONDataWithIdAndTheTimeLeftShouldBeLessThenMinute(int p0)
    {
        throw new PendingStepException();
    }

    [Then(@"Wait for (.*) minute")]
    public void ThenWaitForMinute(int p0)
    {
        throw new PendingStepException();
    }

    [When(@"Get timer status API is called using the Id")]
    public void WhenGetTimerStatusAPIIsCalledUsingTheId()
    {
        throw new PendingStepException();
    }

    [Then(@"Should return JSON data with Id and the time left should be (.*) minutes")]
    public void ThenShouldReturnJSONDataWithIdAndTheTimeLeftShouldBeMinutes(int p0)
    {
        throw new PendingStepException();
    }


}
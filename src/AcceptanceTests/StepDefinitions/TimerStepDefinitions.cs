using AcceptanceTests.Support;
using RestSharp;
using System.Net;
using TimerApi.ApiModels;

namespace AcceptanceTests.StepDefinitions;

[Binding]
public sealed class TimerStepDefinitions
{
    private const string Response = "response";
    private const string Id = "id";

    private readonly ScenarioContext _context;

    public TimerStepDefinitions(ScenarioContext context) => _context = context;

    [Given(@"The Rest Api endpoint is live")]
    public async Task GivenTheRestApiEndpointIsLive()
    {
        using var client = new HttpClient();
        using var response = await client.GetAsync($"{ConfigurationHelper.ApiUrl}health");
        response.EnsureSuccessStatusCode();
    }

    [When(@"Set timer is called with no JSON payload")]
    public async Task WhenSetTimerIsCalledWithNoJSONPayload() =>
        _context[Response] = await CallSetTimer(null);

    [Then(@"Should return Bad Request")]
    public void ThenShouldReturnBadRequest() => ((RestResponse)_context[Response]).StatusCode.Should().Be(HttpStatusCode.BadRequest);

    [When(@"Set timer is called with empty Callback url")]
    public async Task WhenSetTimerIsCalledWithEmptyCallbackUrl() => 
          _context[Response] = await CallSetTimer(CreateTimerRequest(0, 0, 0, ""));

    [When(@"Set timer is called with (.*) hours (.*) minute and (.*) seconds and callback url ""([^""]*)""")]
    public async Task WhenSetTimerIsCalledWithHoursMinuteAndSecondsAndCallbackUrl(int hours, int minutes, int seconds, string url) =>
        _context[Response] = await CallSetTimer(CreateTimerRequest(hours, minutes, seconds, url));

    [Then(@"Should return an Id")]
    public void ThenShouldReturnAnId()
    {
        var response = _context[Response] as RestResponse<SetTimerResponse>;
        response.IsSuccessful.Should().BeTrue();
        response.Data.Id.Should().NotBeNullOrEmpty();
        _context[Id] = response.Data.Id;
    }
    [When(@"Get tiemer status API is called using the Id")]
    public async Task WhenGetTiemerStatusAPIIsCalledUsingTheId()
    {
        _context["response"] = await CallGetTimer(_context[Id] as string);
    }

    [Then(@"Should return JSON data with Id and the time left should be less then (.*) minutes")]
    public void ThenShouldReturnJSONDataWithIdAndTheTimeLeftShouldBeLessThenMinutes(int minutes)
    {        
        var response = _context[Response] as RestResponse<GetTimerResponse>;
        response.Data.Id.Should().BeEquivalentTo(_context[Id] as string);
        response.Data.time_left.Should().BeLessThan(minutes * 60);
    }

    [Then(@"Should return JSON data with Id and the time left should be (.*) minute")]
    public void ThenShouldReturnJSONDataWithIdAndTheTimeLeftShouldBeMinute(int minute)
    {
        var response = _context[Response] as RestResponse<GetTimerResponse>;
        response.Data.Id.Should().BeEquivalentTo(_context[Id] as string);
        response.Data.time_left.Should().Be(minute);
    }

    [When(@"Get timer is call with empty Id")]
    public async Task WhenGetTimerIsCallWithEmptyId()
    {
        _context[Response] = await CallGetTimer("");
    }

    [When(@"Get timer is call with a not known Id")]
    public async Task WhenGetTimerIsCallWithANotKnownId()
    {
        _context[Response] = await CallGetTimer("invalidid");
    }

    [Then(@"Should return Not Found")]
    public void ThenShouldReturnNotFound()
    {
        var response = _context[Response] as RestResponse<GetTimerResponse>;
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task<RestResponse<SetTimerResponse>> CallSetTimer(SetTimerRequest request) =>
        await new RestClient().ExecuteAsync<SetTimerResponse> (new RestRequest(ConfigurationHelper.TimersUrl).AddJsonBody(request), Method.Post);
    private async Task<RestResponse<GetTimerResponse>> CallGetTimer(string id) =>
        await new RestClient().ExecuteAsync<GetTimerResponse>(new RestRequest($"{ConfigurationHelper.TimersUrl}/{id}"), Method.Get);

    private SetTimerRequest CreateTimerRequest(int hours, int minutes, int seconds, string url) =>
        new SetTimerRequest
        {
            Hours = hours,
            Minutes = minutes,
            Seconds = seconds,
            CallbackUrl = url
        };
    private class SetTimerResponse
    {
        public string Id { get; set; }
    }
    private class GetTimerResponse
    {
        public string Id { get; set; }
        public int time_left { get; set; }
    }
}
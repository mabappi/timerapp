using Microsoft.AspNetCore.Mvc;
using TimerApi.ApiModels;
using UnitTests.Builders;
namespace UnitTests.Controllers;
public class TimersControllerTests
{
    [Fact]
    public async Task GetTimer_EmptyId_ShouldReturnBadRequest()
    {
        var result = await new TimersControllerBuilder().Build().GetTimer(string.Empty) as BadRequestResult;
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task GetTimer_WithId_ShouldReturnTimeLeft()
    {
        var id = Guid.NewGuid().ToString();
        var result = await new TimersControllerBuilder().WithIdAndTime(id, 100).Build().GetTimer(id) as JsonResult;
        Assert.NotNull(result);
        Assert.NotNull(result.Value);
    }
    [Fact]
    public async Task SetTimer_WithNullRequest_ShouldReturnBadRequest()
    {
        var result = await new TimersControllerBuilder().Build().SetTimer(null) as BadRequestResult;
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }
    [Fact]
    public async Task SetTimer_WithValidRequest_ShouldReturnId()
    {
        var result = await new TimersControllerBuilder().Build().SetTimer(new SetTimerRequest { CallbackUrl = "SomeUrl"}) as JsonResult;
        Assert.NotNull(result);
    }
}
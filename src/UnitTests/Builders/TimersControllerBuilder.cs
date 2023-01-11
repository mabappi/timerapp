using Moq;
using TimerApi.ApiModels;
using TimerApi.Controllers;
using TimerApi.Services;
namespace UnitTests.Builders;
internal class TimersControllerBuilder : BuilderBase<TimersController>
{
    Mock<ITimerService> _timerService = new Mock<ITimerService>();
    protected override TimersController BuildInternal() =>
        new TimersController(_timerService.Object);
    public TimersControllerBuilder WithIdAndTime(string id, int timeLeft)
    {
        _timerService.Setup(x => x.GetTimer(id)).Returns(Task.FromResult(timeLeft));
        return this;
    }
    public TimersControllerBuilder WithSetTimerId(string id)
    {
        _timerService.Setup(x => x.SetTimer(It.IsAny<SetTimerRequest>())).Returns(Task.FromResult(id));
        return this;
    }
}

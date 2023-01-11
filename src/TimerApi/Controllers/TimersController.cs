using Microsoft.AspNetCore.Mvc;
using TimerApi.ApiModels;
using TimerApi.Services;
namespace TimerApi.Controllers;
[ApiController]
[Route("[controller]")]
public class TimersController : Controller
{
    private readonly ITimerService _timerService;
    public TimersController(ITimerService timerService) => _timerService = timerService;
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTimer([FromRoute]string id) => Json(new { id, time_left = await _timerService.GetTimer(id) });
    [HttpPost]
    public async Task<IActionResult> SetTimer(SetTimerRequest request) => Json(new {id = await _timerService.SetTimer(request)});
}
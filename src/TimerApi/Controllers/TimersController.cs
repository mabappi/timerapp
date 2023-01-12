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
    public async Task<IActionResult> GetTimer([FromRoute]string id) => 
        string.IsNullOrEmpty(id)
        ? BadRequest()
        : await GetTimerInternal(id);
        
    [HttpPost]
    public async Task<IActionResult> SetTimer(SetTimerRequest request) => 
        request == null
        ? BadRequest()
        : Json(new {id = await _timerService.SetTimer(request)});

    private async Task<IActionResult> GetTimerInternal(string id)
    {
        var result = await _timerService.GetTimer(id);
        return result == -1 ? NotFound() : Json(new { id, time_left = result });
    }
}
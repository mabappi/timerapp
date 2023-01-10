using Microsoft.AspNetCore.Mvc;

namespace TimerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimersController : Controller
    {

        private readonly ILogger<TimersController> _logger;

        public TimersController(ILogger<TimersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]string id) => Json(new { id, time_left = 1 });

        [HttpPost]
        public async Task<IActionResult> Set() => Json(new {id = 1});
    }
}
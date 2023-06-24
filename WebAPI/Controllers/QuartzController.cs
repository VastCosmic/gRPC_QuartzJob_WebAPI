using Microsoft.AspNetCore.Mvc;
using QuartzJob.Command;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuartzController : ControllerBase
    {
        private readonly string _address;
        private readonly ILogger<QuartzController> _logger;

        public QuartzController(string address, ILogger<QuartzController> logger)
        {
            _address = address;
            _logger = logger;
        }

        [HttpGet("read/start")]
        public async Task<IActionResult> StartRead()
        {
            try
            {
                new ReadCommand(_address);
                await ReadCommand.Start();
                _logger.LogInformation("Read command start successfully.");
                return Ok(new { status = "ok" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Read command start failed.");
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        [HttpGet("read/stop")]
        public async Task<IActionResult> StopRead()
        {
            try
            {
                await ReadCommand.Stop();
                _logger.LogInformation("Read command stop successfully.");
                return Ok(new { status = "ok" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Read command stop failed.");
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }


        public class WriteRequest
        {
            public string? Cron { get; set; }
        }

        [HttpPost("write/start")]
        public async Task<IActionResult> StartWrite([FromBody] WriteRequest request)
        {
            try
            {
                if (request.Cron == null)
                {
                    _logger.LogError("Cron can not be null !");
                    throw new InvalidOperationException("Cron can not be null !");
                }
                new WriteCommand(_address, request.Cron);
                _logger.LogInformation("Write command start successfully.");
                await WriteCommand.Start();
                return Ok(new { status = "ok" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Write command start failed.");
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        [HttpGet("write/stop")]
        public async Task<IActionResult> StopWrite()
        {
            try
            {
                await WriteCommand.Stop();
                _logger.LogInformation("Write command stop successfully.");
                return Ok(new { status = "ok" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Write command stop failed.");
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }
    }
}
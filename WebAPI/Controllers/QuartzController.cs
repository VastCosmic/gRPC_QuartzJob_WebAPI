using Microsoft.AspNetCore.Mvc;
using QuartzJob.Command;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuartzController : ControllerBase
    {
        private readonly string _address;

        public QuartzController(string address)
        {
            _address = address;
        }

        [HttpGet("read/start")]
        public async Task<IActionResult> StartRead()
        {
            try
            {
                new ReadCommand(_address);
                await ReadCommand.Start();
                return Ok(new { status = "ok" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        [HttpGet("read/stop")]
        public async Task<IActionResult> StopRead()
        {
            try
            {
                await ReadCommand.Stop();
                return Ok(new { status = "ok" });
            }
            catch (Exception ex)
            {
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
                    throw new InvalidOperationException("Cron can not be null !");
                }
                new WriteCommand(_address, request.Cron);
                await WriteCommand.Start();
                return Ok(new { status = "ok" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        [HttpGet("write/stop")]
        public async Task<IActionResult> StopWrite()
        {
            try
            {
                await WriteCommand.Stop();
                return Ok(new { status = "ok" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }
    }
}
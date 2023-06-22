using Microsoft.AspNetCore.Mvc;
using Quartz;
using QuartzJob.Job;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuartzController : ControllerBase
    {
        private readonly IScheduler _scheduler;
        private readonly IConfiguration _configuration;

        public QuartzController(IScheduler scheduler, IConfiguration configuration)
        {
            _scheduler = scheduler;
            _configuration = configuration;
        }

        [HttpGet("Run")]
        public async Task<OkObjectResult> Run(string jobName)
        {
            //检查任务是否存在
            //如果不存在则创建一个新的任务
            var key = new JobKey($"ReadCommandAsyncJob");
            var jobExist = await _scheduler.CheckExists(key);
            if (!jobExist)
            {
                // 使用 https://localhost:7259 作为监听地址
                var address = "https://localhost:7259";

                // 创建一个新的任务，并传递监听地址
                var job = JobBuilder.Create<ReadCommandAsyncJob>()
                    .WithIdentity(key)
                    .UsingJobData("address", address)
                    .Build();

                // 创建一个新的触发器
                var trigger = TriggerBuilder.Create()
                    .WithIdentity("ReadCommandAsyncJobTrigger")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithInterval(TimeSpan.FromMilliseconds(500)) // 设置时间间隔为500ms
                        .RepeatForever()
                    )
                    .Build();

                // 将任务和触发器添加到调度器中
                await _scheduler.ScheduleJob(job, trigger);
            }

            await _scheduler.TriggerJob(key);
            return Ok("OK");
        }

        [HttpGet("Cancel")]
        public async Task<OkObjectResult> Cancel(string jobName)
        {
            var key = new JobKey($"ReadCommandAsyncJob");
            var jobExist = await _scheduler.CheckExists(key);
            if (jobExist)
            {
                await _scheduler.DeleteJob(key);
            }

            return Ok("OK");
        }
    }
}

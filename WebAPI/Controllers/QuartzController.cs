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
            //��������Ƿ����
            //����������򴴽�һ���µ�����
            var key = new JobKey($"ReadCommandAsyncJob");
            var jobExist = await _scheduler.CheckExists(key);
            if (!jobExist)
            {
                // ʹ�� https://localhost:7259 ��Ϊ������ַ
                var address = "https://localhost:7259";

                // ����һ���µ����񣬲����ݼ�����ַ
                var job = JobBuilder.Create<ReadCommandAsyncJob>()
                    .WithIdentity(key)
                    .UsingJobData("address", address)
                    .Build();

                // ����һ���µĴ�����
                var trigger = TriggerBuilder.Create()
                    .WithIdentity("ReadCommandAsyncJobTrigger")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithInterval(TimeSpan.FromMilliseconds(500)) // ����ʱ����Ϊ500ms
                        .RepeatForever()
                    )
                    .Build();

                // ������ʹ�������ӵ���������
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

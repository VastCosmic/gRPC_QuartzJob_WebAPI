using Client.QuartzJob.Job;
using Quartz.Impl;
using Quartz;

namespace Client.QuartzJob
{
    internal class ReadCommand
    {
        /// <summary>
        /// 读命令
        /// </summary>
        /// <returns></returns>
        public static async Task Read()
        {
            // 新建一个调度器
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler().ConfigureAwait(false);

            // 新建一个任务
            var job = JobBuilder.Create<ReadCommandAsyncJob>()
                .WithIdentity("ReadCommandAsyncJob", "ReadJob")
                .Build();

            // 新建一个触发器
            var trigger = TriggerBuilder.Create()
                .WithIdentity("ReadCommandAsyncJobTrigger", "ReadTrigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMilliseconds(500)) // 设置时间间隔为500ms
                    .RepeatForever()
                )
                .Build();

            // 将任务和触发器添加到调度器中
            await scheduler.ScheduleJob(job, trigger).ConfigureAwait(false);

            // 开启调度器
            await scheduler.Start().ConfigureAwait(false);

            // 等待
            await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
        }
    }
}

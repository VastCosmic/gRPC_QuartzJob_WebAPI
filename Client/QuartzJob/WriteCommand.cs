using Client.QuartzJob.Job;
using Quartz;
using Quartz.Impl;

namespace Client.QuartzJob
{
    internal class WriteCommand
    {
        /// <summary>
        /// 写命令
        /// </summary>
        /// <returns></returns>
        public static async Task Write(string cronStr)
        {
            // 新建一个调度器
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler().ConfigureAwait(false);

            // 新建一个任务
            var job = JobBuilder.Create<WriteCommandAsyncJob>()
                .WithIdentity("WriteCommandAsyncJob", "WriteJob") // 修改任务的Identity
                .Build();

            // 新建一个触发器
            var trigger = TriggerBuilder.Create()
                .WithIdentity("WriteCommandAsyncJobTrigger", "WriteTrigger") // 修改触发器的Identity
                .WithCronSchedule(cronStr) // 使用Cron表达式指定执行时间
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


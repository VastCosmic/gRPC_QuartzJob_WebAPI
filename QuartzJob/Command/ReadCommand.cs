using QuartzJob.Job;
using Quartz.Impl;
using Quartz;

namespace QuartzJob.Command
{
    /// <summary>
    /// 读命令
    /// </summary>
    public class ReadCommand
    {
        // 监听地址
        private static string? Address { get; set; }

        // 调度器
        private static IScheduler? scheduler; 

        public ReadCommand(string address)
        {
            Address = address;
            scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public static async Task Start()
        {
            if (Address == null)
            {
                throw new InvalidOperationException("Address can not be null");
            }
            if (scheduler == null)
            {
                throw new InvalidOperationException("Scheduler can not be null");
            }

            // 新建一个任务
            var job = JobBuilder.Create<ReadCommandAsyncJob>()
                .WithIdentity("ReadCommandAsyncJob", "ReadJob")
                .UsingJobData("address", Address)
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
            //await Task.Delay(Timeout.Infinite).ConfigureAwait(false);
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public static async Task Stop()
        {
            if (scheduler == null)
            {
                throw new InvalidOperationException("Scheduler can not be null");
            }
            await scheduler.Shutdown().ConfigureAwait(false);
        }
    }
}

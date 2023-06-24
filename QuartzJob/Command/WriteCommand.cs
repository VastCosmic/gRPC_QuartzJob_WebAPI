using Quartz.Impl;
using Quartz;
using QuartzJob.Job;
using QuartzJob.Trigger;

namespace QuartzJob.Command
{
    /// <summary>
    /// 写命令
    /// </summary>
    public class WriteCommand
    {
        // 监听地址
        private static string? Address { get; set; }

        // Cron表达式
        private static string? CronStr { get; set; }

        // 调度器
        private static IScheduler? scheduler;

        /// <summary>
        /// 写命令构造函数，要传入监听的gRPC服务器地址、Cron表达式
        /// </summary>
        /// <param name="address">监听的gRPC服务器地址</param>
        /// <param name="cronStr">Cron表达式</param>
        public WriteCommand(string address, string cronStr)
        {
            Address = address;
            CronStr = cronStr;
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
            if (CronStr == null)
            {
                throw new InvalidOperationException("CronStr can not be null");
            }
            if (scheduler == null)
            {
                throw new InvalidOperationException("Scheduler can not be null");
            }

            // 新建一个任务
            var jobDetail = JobBuilder.Create<WriteCommandAsyncJob>()
                .WithIdentity("WriteCommandAsyncJob", "WriteJob")
                .UsingJobData("address", Address)
                .Build();

            // 新建一个触发器
            var jobTrigger = WriteCommandTrigger.Create(CronStr);

            // 将任务和触发器添加到调度器中
            await scheduler.ScheduleJob(jobDetail, jobTrigger).ConfigureAwait(false);

            // 开启调度器
            await scheduler.Start().ConfigureAwait(false);
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

using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuartzJob.Job;
using System.Net;

namespace QuartzJob.Command
{
    public class WriteCommand
    {
        // 监听地址
        private static string? Address { get; set; }

        // Cron表达式
        private static string? CronStr { get; set; }

        // 调度器
        private static IScheduler? scheduler;

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
            if (scheduler == null)
            {
                throw new InvalidOperationException("Scheduler can not be null");
            }
            if (Address == null)
            {
                throw new InvalidOperationException("Address can not be null");
            }
            if (CronStr == null)
            {
                throw new InvalidOperationException("CronStr can not be null");
            }

            // 新建一个任务
            var job = JobBuilder.Create<WriteCommandAsyncJob>()
                .WithIdentity("WriteCommandAsyncJob", "WriteJob")
                .UsingJobData("address", Address)
                .Build();

            // 新建一个触发器
            var trigger = TriggerBuilder.Create()
                .WithIdentity("WriteCommandAsyncJobTrigger", "WriteTrigger")
                .WithCronSchedule(CronStr) // 使用Cron表达式指定执行时间
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
                throw new InvalidOperationException("null scheduler");
            }
            await scheduler.Shutdown().ConfigureAwait(false);
        }
    }
}

using QuartzJob.Job;
using Quartz.Impl;
using Quartz;
using QuartzJob.Trigger;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;

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
        // 配置
        private static IConfiguration? configuration;

        /// <summary>
        /// 读命令构造函数，要传入监听的gRPC服务器地址
        /// </summary>
        /// <param name="address">监听的gRPC服务器地址</param>
        public ReadCommand(string address)
        {
            Address = address;

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("N:\\VC_VS_PROJECT\\ebara\\QuartzJob\\appsettings.Development.json");
            configuration = builder.Build();

            // 获取调度器工厂实例
            var schedulerFactory = new StdSchedulerFactory(configuration.GetSection("Quartz").Get<NameValueCollection>());

            // 获取调度器实例
            scheduler = schedulerFactory.GetScheduler().Result;
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
            var jobDetail = JobBuilder.Create<ReadCommandAsyncJob>()
                .WithIdentity("ReadCommandAsyncJob", "ReadJob")
                .UsingJobData("address", Address)
                .Build();

            // 新建一个触发器
            var jobTrigger = ReadCommandTrigger.Create();

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

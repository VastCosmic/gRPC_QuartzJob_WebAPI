﻿using Grpc.Net.Client;
using Quartz;
using SwitchService;

namespace QuartzJob.Job
{
    /// <summary>
    /// 读命令任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class ReadCommandJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            // 从JobDataMap中获取监听地址
            var dataMap = context.MergedJobDataMap;
            var address = dataMap.GetString("address") ?? throw new ArgumentNullException("address");
            // 创建Channel和Client
            var channel = GrpcChannel.ForAddress(address);
            var client = new SwitchApi.SwitchApiClient(channel);

            try
            {
                return Task.Factory.StartNew(() =>
                {
                    // 模拟读命令
                    var replyExecRpcCommand = client.ExecRpcCommand(new Request { StrRequest = "ReadCommand" });
                    Console.WriteLine(replyExecRpcCommand.StrRply);
                });
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(ex, false);
            }
        }
    }
}

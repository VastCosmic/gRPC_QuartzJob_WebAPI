﻿using Grpc.Net.Client;
using Quartz;
using SwitchService;

namespace QuartzJob.Job
{
    /// <summary>
    /// 读命令任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class ReadCommandSyncJob : IJob
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
                    var replyExecRpcCommandSync = client.ExecRpcCommandSync(new Request { StrRequest = "ReadCommandSync" });
                    Console.WriteLine(replyExecRpcCommandSync.StrRply);
                });
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(ex, false);
            }
        }
    }
}

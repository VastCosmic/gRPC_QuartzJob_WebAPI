﻿using Grpc.Net.Client;
using Quartz;
using SwitchService;

namespace Client.QuartzJob.Job
{
    /// <summary>
    /// 读命令任务
    /// </summary>
    internal class ReadCommandJob : IJob
    {
        // 创建静态的Channel和Client
        private static readonly GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7259");
        private static readonly SwitchApi.SwitchApiClient client = new SwitchApi.SwitchApiClient(channel);

        public Task Execute(IJobExecutionContext context)
        {
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
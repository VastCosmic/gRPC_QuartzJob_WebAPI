using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SwitchService;
using System;

namespace QuartzJob
{
    public class ReadCommandJob : IJob
    {
        private readonly SwitchApi.SwitchApiClient client;

        public ReadCommandJob(SwitchApi.SwitchApiClient client)
        {            
            // 新建一个Channel，监听地址
            var channel = GrpcChannel.ForAddress("https://localhost:7259");
            // 新建客户端
            client = new SwitchApi.SwitchApiClient(channel);
            this.client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return  Task.Factory.StartNew(() =>
            {
                Console.WriteLine("ReadCommandJob is executing.");
            });
        }
    }
}
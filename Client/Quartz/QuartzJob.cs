using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SwitchService;

namespace QuartzJob
{
    public class ReadCommandJob : IJob
    {
        private readonly SwitchApi.SwitchApiClient _client;

        public ReadCommandJob(SwitchApi.SwitchApiClient client)
        {
            _client = client;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // 读取服务器端发送的命令
            var request = new Request { StrRequest = "Some command" };
            // 调用ExecRpcCommand或ExecRpcCommandSync方法执行命令
            var reply = await _client.ExecRpcCommandAsync(request);
            // 处理回复
            Console.WriteLine(reply.StrRply);
        }
    }


}

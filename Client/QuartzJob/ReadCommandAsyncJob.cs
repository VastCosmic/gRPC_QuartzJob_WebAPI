using Grpc.Net.Client;
using Quartz;
using SwitchService;

namespace Client.QuartzJob
{
    /// <summary>
    /// 异步读命令任务
    /// </summary>
    internal class ReadCommandAsyncJob : IJob
    {
        // 创建静态的Channel和Client
        private static readonly GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7259");
        private static readonly SwitchApi.SwitchApiClient client = new SwitchApi.SwitchApiClient(channel);

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // 模拟异步读命令
                var replyExecRpcCommand = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is ExecRpcCommandAsync test." });
                Console.WriteLine(replyExecRpcCommand.StrRply);
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(ex, false);
            }
        }

    }
}

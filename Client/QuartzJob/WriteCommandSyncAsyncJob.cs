using Grpc.Net.Client;
using Quartz;
using SwitchService;
namespace Client.QuartzJob
{
    /// <summary>
    /// 异步写命令任务
    /// </summary>
    internal class WriteCommandSyncAsyncJob : IJob
    {
        // 创建静态的Channel和Client
        private static readonly GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7259");
        private static readonly SwitchApi.SwitchApiClient client = new SwitchApi.SwitchApiClient(channel);

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // 模拟异步写命令
                var replyExecRpcCommandSyncAsync = await client.ExecRpcCommandSyncAsync(new Request { StrRequest = "WriteCommandSyncAsync" });
                Console.WriteLine(replyExecRpcCommandSyncAsync.StrRply);
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(ex, false);
            }
        }
    }
}

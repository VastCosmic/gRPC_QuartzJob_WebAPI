using Grpc.Net.Client;
using Quartz;
using SwitchService;

namespace QuartzJob.Job
{
    /// <summary>
    /// 异步读命令任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class ReadCommandSyncAsyncJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {        
            // 从JobDataMap中获取监听地址
            var dataMap = context.MergedJobDataMap;
            var address = dataMap.GetString("address") ?? throw new ArgumentNullException("address");
            // 创建Channel和Client
            var channel = GrpcChannel.ForAddress(address);
            var client = new SwitchApi.SwitchApiClient(channel);

            try
            {
                // 模拟异步读命令
                var replyExecRpcCommandSyncAsync = await client.ExecRpcCommandSyncAsync(new Request { StrRequest = "ReadCommandSyncAsync" });
                Console.WriteLine(replyExecRpcCommandSyncAsync.StrRply);
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(ex, false);
            }
        }
    }
}

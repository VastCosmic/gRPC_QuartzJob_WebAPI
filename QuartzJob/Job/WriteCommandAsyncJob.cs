using Grpc.Net.Client;
using Quartz;
using SwitchService;

namespace QuartzJob.Job
{
    /// <summary>
    /// 异步写命令任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class WriteCommandAsyncJob : IJob
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
                // 模拟异步写命令
                var replyExecRpcCommandAsync = await client.ExecRpcCommandAsync(new Request { StrRequest = "WriteCommandAsync" });
                Console.WriteLine(replyExecRpcCommandAsync.StrRply);
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(ex, false);
            }
        }
    }
}

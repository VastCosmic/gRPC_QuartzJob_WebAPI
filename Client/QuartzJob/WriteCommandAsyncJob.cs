using Grpc.Net.Client;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwitchService;

namespace Client.QuartzJob
{
    /// <summary>
    /// 异步写命令任务
    /// </summary>
    internal class WriteCommandAsyncJob
    {
        // 创建静态的Channel和Client
        private static readonly GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7259");
        private static readonly SwitchApi.SwitchApiClient client = new SwitchApi.SwitchApiClient(channel);

        public async Task Execute(IJobExecutionContext context)
        {
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

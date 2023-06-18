using Grpc.Net.Client;
using SwitchService;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 新建一个Channel，监听地址
            var channel = GrpcChannel.ForAddress("https://localhost:7259");
            // 新建客户端
            var client = new SwitchApi.SwitchApiClient(channel);

            // 服务端测试
            // 模拟读
            var readReplySync = client.ExecRpcCommandSync(new Request { StrRequest = "Read test." });
            Console.WriteLine(readReplySync.StrRply);
            var readReply = client.ExecRpcCommand(new Request { StrRequest = "Read test." });
            Console.WriteLine(readReply.StrRply);

            // 模拟写
            var writeReplySync = client.ExecRpcCommandSync(new Request { StrRequest = "Write test." });
            Console.WriteLine(writeReplySync.StrRply);
            var writeReply = client.ExecRpcCommand(new Request { StrRequest = "Write test." });
            Console.WriteLine(writeReply.StrRply);

            // 模拟任务启动
            var startReplySync = client.ExecRpcCommandSync(new Request { StrRequest = "Start test." });
            Console.WriteLine(startReplySync.StrRply);
            var startReply = client.ExecRpcCommand(new Request { StrRequest = "Start test." });
            Console.WriteLine(startReply.StrRply);

            // 模拟任务停止
            var stopReplySync = client.ExecRpcCommandSync(new Request { StrRequest = "Stop test." });
            Console.WriteLine(stopReplySync.StrRply);
            var stopReply = client.ExecRpcCommand(new Request { StrRequest = "Stop test." });
            Console.WriteLine(stopReply.StrRply);
        }
    }
}
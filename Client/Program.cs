using Grpc.Net.Client;
using SwitchService;

// 新建一个Channel，监听地址
var channel = GrpcChannel.ForAddress("https://localhost:7259");
// 新建客户端
var client = new SwitchApi.SwitchApiClient(channel);

// 服务端测试
var replyAsync = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is ExecRpcCommandAsync test." });
Console.WriteLine(replyAsync.StrRply);
var reply = client.ExecRpcCommand(new Request { StrRequest = "This is ExecRpcCommand test." });
Console.WriteLine(reply.StrRply);

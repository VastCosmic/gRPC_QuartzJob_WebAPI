using Grpc.Net.Client;
using SwitchService;

/*****************************************************************
 * 以下为gRPC通讯测试代码
*****************************************************************/

// 新建一个Channel，监听地址
var channel = GrpcChannel.ForAddress("https://localhost:7259");
// 新建客户端
var client = new SwitchApi.SwitchApiClient(channel);

//gRPC客户端发送请求
var replyExecRpcCommandAsync = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is ExecRpcCommandAsync test." });
Console.WriteLine(replyExecRpcCommandAsync.StrRply);
var replyExecRpcCommand = client.ExecRpcCommand(new Request { StrRequest = "This is ExecRpcCommand test." });
Console.WriteLine(replyExecRpcCommand.StrRply);
var replyExecRpcCommandSyncAsync = await client.ExecRpcCommandSyncAsync(new Request { StrRequest = "This is ExecRpcCommandSyncAsync test." });
Console.WriteLine(replyExecRpcCommandSyncAsync.StrRply);
var replyExecRpcCommandSync = client.ExecRpcCommandSync(new Request { StrRequest = "This is ExecRpcCommandSync test." });
Console.WriteLine(replyExecRpcCommandSync.StrRply);

//模拟读、写、启动、停止
var replyRead = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is Read test." });
Console.WriteLine(replyRead.StrRply);
var replyWrite = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is Write test." });
Console.WriteLine(replyWrite.StrRply);
var replyStart = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is Start test." });
Console.WriteLine(replyStart.StrRply);
var replyStop = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is Stop test." });
Console.WriteLine(replyStop.StrRply);
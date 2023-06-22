using Grpc.Net.Client;
using Quartz.Impl;
using Quartz;
using SwitchService;
using Client.QuartzJob;

// 新建一个Channel，监听地址
var channel = GrpcChannel.ForAddress("https://localhost:7259");
// 新建客户端
var client = new SwitchApi.SwitchApiClient(channel);

// 新建一个调度器
var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
// 开启调度器
await scheduler.Start();

// 新建一个任务
var job = JobBuilder.Create<ReadCommandAsyncJob>()
    .WithIdentity("ReadCommandAsyncJob", "ReadJob")
    .Build();

// 新建一个触发器
var trigger = TriggerBuilder.Create()
    .WithIdentity("ReadCommandAsyncJobTrigger", "ReadTrigger")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithInterval(TimeSpan.FromMilliseconds(500)) // 设置时间间隔为500ms
        .RepeatForever()) // 无限重复
    .Build();

// 将任务和触发器添加到调度器中
await scheduler.ScheduleJob(job, trigger);

// 等待
await Task.Delay(TimeSpan.FromSeconds(5));
Console.WriteLine("5s LATER...");

await scheduler.Interrupt(new JobKey("ReadCommandAsyncJob", "ReadJob"));

// 关闭调度器
await scheduler.Shutdown();


// gRPC客户端发送请求
//var replyExecRpcCommandAsync = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is ExecRpcCommandAsync test." });
//Console.WriteLine(replyExecRpcCommandAsync.StrRply);
//var replyExecRpcCommand = client.ExecRpcCommand(new Request { StrRequest = "This is ExecRpcCommand test." });
//Console.WriteLine(replyExecRpcCommand.StrRply);
//var replyExecRpcCommandSync = await client.ExecRpcCommandSyncAsync(new Request { StrRequest = "This is ExecRpcCommandSyncAsync test." });
//Console.WriteLine(replyExecRpcCommandSync.StrRply);
//var replyExecRpcCommandSync = client.ExecRpcCommandSync(new Request { StrRequest = "This is ExecRpcCommandSync test." });
//Console.WriteLine(replyExecRpcCommandSync.StrRply);

// 模拟读、写、启动、停止
//var replyRead = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is Read test." });
//Console.WriteLine(replyRead.StrRply);
//var replyWrite = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is Write test." });
//Console.WriteLine(replyWrite.StrRply);
//var replyStart = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is Start test." });
//Console.WriteLine(replyStart.StrRply);
//var replyStop = await client.ExecRpcCommandAsync(new Request { StrRequest = "This is Stop test." });
//Console.WriteLine(replyStop.StrRply);
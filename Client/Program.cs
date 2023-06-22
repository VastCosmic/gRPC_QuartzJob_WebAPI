using Grpc.Net.Client;
using Quartz.Impl;
using Quartz;
using SwitchService;
using Client.QuartzJob.Job;
using Client.QuartzJob;

//// 读命令测试
//new ReadCommand();
//await ReadCommand.Read();

// 写命令测试
new WriteCommand();
string cronStr = "1/2 * * * * ?";
await WriteCommand.Write(cronStr);


/*****************************************************************
 * job test code
*****************************************************************/

//// 新建一个调度器
//var scheduler = await StdSchedulerFactory.GetDefaultScheduler().ConfigureAwait(false);
//// 开启调度器
//await scheduler.Start().ConfigureAwait(false);

//// 新建一个任务
//var job = JobBuilder.Create<ReadCommandAsyncJob>()
//    .WithIdentity("ReadCommandAsyncJob", "ReadJob")
//    .Build();

//// 新建一个触发器
//var trigger = TriggerBuilder.Create()
//    .WithIdentity("ReadCommandAsyncJobTrigger", "ReadTrigger")
//    .StartNow()
//    .WithSimpleSchedule(x => x
//        .WithInterval(TimeSpan.FromMilliseconds(500)) // 设置时间间隔为500ms
//        .RepeatForever()
//    )
//    .Build();

//// 将任务和触发器添加到调度器中
//await scheduler.ScheduleJob(job, trigger).ConfigureAwait(false);

////Console.WriteLine("5s LATER...");

//// 等待
//await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

//await scheduler.Interrupt(new JobKey("ReadCommandAsyncJob", "ReadJob")).ConfigureAwait(false);

//// 关闭调度器
//await scheduler.Shutdown(true).ConfigureAwait(false);



/*****************************************************************
 * 以下为gRPC通讯测试代码
*****************************************************************/

//// 新建一个Channel，监听地址
//var channel = GrpcChannel.ForAddress("https://localhost:7259");
//// 新建客户端
//var client = new SwitchApi.SwitchApiClient(channel);

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
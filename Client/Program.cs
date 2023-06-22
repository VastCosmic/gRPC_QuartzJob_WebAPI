using Grpc.Net.Client;
using Quartz.Impl;
using Quartz;
using SwitchService;
using Client.QuartzJob;


/*****************************************************************
 * 以下为 job 测试代码
*****************************************************************/

//// 读命令测试
//new ReadCommand();
//await ReadCommand.Read();

// 写命令测试
new WriteCommand();
string cronStr = "1/2 * * * * ?";
await WriteCommand.Write(cronStr);
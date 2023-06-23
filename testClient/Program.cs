using QuartzJob.Command;

var address="https://localhost:7259";

//以下为测试程序，用以进行 job 的调度测试

new ReadCommand(address);
Console.WriteLine("Try to ReadCommand! Input any key to end the job...");

// Start the job
await ReadCommand.Start();
// Wait for user input before ending the job
Console.ReadKey(true);
// Stop the job
await ReadCommand.Stop();
Console.WriteLine("Stop ReadCommand Completed!");

string cronStr = "1/2 * * * * ?";
new WriteCommand(address, cronStr);
Console.WriteLine("Try to WriteCommand! Input any key to end the job...");
// Start the job
await WriteCommand.Start();
// Wait for user input before ending the job
Console.ReadKey(true);
// Stop the job
await WriteCommand.Stop();
Console.WriteLine("Stop WriteCommand Completed!");
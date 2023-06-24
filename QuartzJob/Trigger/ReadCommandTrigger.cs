using Quartz;

namespace QuartzJob.Trigger
{
    /// <summary>
    /// 读命令触发器
    /// </summary>
    public class ReadCommandTrigger
    {
        public static ITrigger Create()
        {
            // 新建一个触发器
            var trigger = TriggerBuilder.Create()
                .WithIdentity("ReadCommandAsyncJobTrigger", "ReadTrigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMilliseconds(500)) // 设置时间间隔为500ms
                    .RepeatForever()
                )
                .Build();
            return trigger;
        }
    }
}

using Quartz;

namespace QuartzJob.Trigger
{
    /// <summary>
    /// 写命令触发器
    /// </summary>
    public class WriteCommandTrigger
    {
        public static ITrigger Create(string CronStr)
        {
            // 新建一个触发器
            var trigger = TriggerBuilder.Create()
                .WithIdentity("WriteCommandAsyncJobTrigger", "WriteTrigger")
                .WithCronSchedule(CronStr) // 使用Cron表达式指定执行时间
                .Build();
            return trigger;
        }

    }
}

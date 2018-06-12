using Quartz;
using Quartz.Impl;

namespace SocarWebPage.WebJob
{
    public class UpdateScheduler
    {
        public static void Start()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.Start();

            var job = JobBuilder.Create<Updater>()
                .WithIdentity("job1", "group1")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(60 * 60).RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
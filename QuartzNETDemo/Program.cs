using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzNETDemo
{
    class Program
    {
        static void Main(string[] args)
        {            
            //首先创建一个作业调度池
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();

            //创建一个具体的作业
            IJobDetail job = JobBuilder.Create<JobDemo>().Build();

            //创建并配置一个触发器
            ISimpleTrigger trigger =
                (ISimpleTrigger)TriggerBuilder.Create()
                .WithSimpleSchedule(
                    x => x.WithIntervalInSeconds(3)
                    .WithRepeatCount(int.MaxValue)
                    ).Build();

            //加入作业调度池
            scheduler.ScheduleJob(job, trigger);

            //开始运行
            scheduler.Start();

            Console.ReadKey();            
        }
    }

    public class JobDemo : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString());
        }
    }
}

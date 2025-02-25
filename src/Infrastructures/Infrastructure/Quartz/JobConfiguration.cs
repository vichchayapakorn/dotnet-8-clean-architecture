using Microsoft.Extensions.Configuration;
using Quartz;

namespace Infrastructure.Quartz
{
    public static class JobConfiguration
    {
        public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration config, string group) where T : IJob
        {
            string jobName = typeof(T).Name;
            var configKey = $"Cron:{group}-Group:{jobName}";
            var cronSchedule = config[configKey] ?? "0 0/5 * 1/1 * ? *";

            var jobKey = new JobKey(jobName, group);
            quartz.AddJob<T>(opt => opt.WithIdentity(jobKey));
            quartz.AddTrigger(opt => opt.ForJob(jobKey).WithIdentity($"{jobName}-trigger").WithCronSchedule(cronSchedule));
        }

        public static void AddJobAndTriggerScale<T>(this IServiceCollectionQuartzConfigurator quartz, IConfiguration config, string group, int numberOfJobs) where T : IJob
        {
            string jobName = typeof(T).Name;

            var configKey = $"Cron:{group}-Group:{jobName}";
            var cronSchedule = config[configKey];

            if (string.IsNullOrEmpty(cronSchedule))
            {
                cronSchedule = "0 0/5 * 1/1 * ? *";
            }
            for (int job = 0; job < numberOfJobs; job++)
            {
                var jobKey = new JobKey($"{jobName}-{job}", group);
                quartz.AddJob<T>(opt => opt.WithIdentity(jobKey).UsingJobData("scale", job));

                quartz.AddTrigger(opt => opt
                    .StartNow()
                    .ForJob(jobKey)
                    .WithIdentity($"{jobName}-{job}-trigger")
                    .WithCronSchedule(cronSchedule));
            }
        }
    }
}

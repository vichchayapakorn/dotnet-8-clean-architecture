using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure.Quartz
{
    public static class JobAddServices
    {
        public static IServiceCollection AddScaleJobService<TJob>(this IServiceCollection services, IConfiguration config, string jobName, int numberOfJob) where TJob : IJob
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();            
                q.UseDefaultThreadPool(config.GetValue("ApplicationConfigs:MaxConcurrency", 70));
                q.AddJobAndTriggerScale<TJob>(config, jobName, numberOfJob);
            });

            services.AddQuartzHostedService(opt =>{ opt.WaitForJobsToComplete = true; });
            return services;
        }
        public static IServiceCollection AddJobService<TJob>(this IServiceCollection services, IConfiguration config, string jobName) where TJob : IJob
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseDefaultThreadPool(int.Parse(config["ApplicationConfigs:MaxConcurrency"]));
                q.AddJobAndTrigger<TJob>(config, jobName);
            });

            services.AddQuartzHostedService(opt =>
            {
                // when shutting down we want jobs to complete gracefully
                opt.WaitForJobsToComplete = true;
            });

            return services;
        }


    }
}

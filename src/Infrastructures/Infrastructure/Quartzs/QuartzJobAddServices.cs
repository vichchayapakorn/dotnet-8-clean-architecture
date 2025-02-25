using Infrastructure.Quartz;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEX.KES.Service.Infrastructure.Quartzs
{
    public static class QuartzJobAddServices
    {
        public static IServiceCollection AddMultiScaleJobService<TJob>(this IServiceCollection services, IConfiguration config, string jobName, int numberOfJob) where TJob : IJob
        {
            services.AddQuartz(q =>
            {
                q.AddJobAndTriggerScale<TJob>(config, jobName, numberOfJob);
            });

            return services;
        }
    }
}

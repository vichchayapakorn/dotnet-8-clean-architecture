using CrystalQuartz.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure.Quartz
{
    public static class QuartzExtensions
    {
        public static async Task UseQuartzAsync(this IApplicationBuilder app)
        {
          
            var schedulerFactory = app.ApplicationServices.GetRequiredService<ISchedulerFactory>();
            try
            {
                var scheduler = await schedulerFactory.GetScheduler();
                 app.UseCrystalQuartz(() => scheduler);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed             
            }
        }

    }
}

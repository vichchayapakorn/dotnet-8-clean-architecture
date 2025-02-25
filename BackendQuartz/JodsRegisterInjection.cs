using BackendQuartz.Jobs;
using Infrastructure.Quartz;


namespace Microsoft.Extensions.DependencyInjection;

public static class JodsRegisterInjection
{

    public static IServiceCollection JodsRegister(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScaleJobService<OrdertCompletedJob>(configuration, "OrdertCompletedGroup", 1);



        return services;
    }

   
}

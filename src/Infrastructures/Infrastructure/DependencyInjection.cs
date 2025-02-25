using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Infrastructure.Data;
using Application.Orders.Commands;
using Application.Common.Interfaces;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Infrastructure.Messaging;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IOrderDbContext>(provider => provider.GetRequiredService<OrderDbContext>());
        //services.AddScoped<IKafkaProducerService>(provider => provider.GetRequiredService<KafkaProducerService>());
        services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
        services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
        //services.AddSingleton<OrderDbContext>();



        return services;
    }

   
}

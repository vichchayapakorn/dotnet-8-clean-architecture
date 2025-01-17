using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Pasistence
{
    public static class Startup
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }


        //public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
           // builder.
    }
}



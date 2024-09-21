using Application.Common.Interfaces;

using Infrastructure.Common;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extension
{
    public static class StartupExtension
    {
        public static void AddCommonService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfiguration>(a => configuration.GetSection("RabbitMqConfiguration"));

            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


            services.AddSingleton<IRabbitMqService, RabbitMqService>();
        }
    }
}

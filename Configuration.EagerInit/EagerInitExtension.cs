using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.EagerInit
{
    public static class EagerInitExtension
    {
        public static IServiceCollection UseEagerInit(this IServiceCollection services)
        {
            List<Type> eagerInitTypes = new List<Type>();
            foreach (var service in services)
            {
                Type serviceType = service.ServiceType;
                EagerInitAttribute? eagerInitAttribute = serviceType.GetCustomAttribute<EagerInitAttribute>();
                if (eagerInitAttribute != null)
                {
                    eagerInitTypes.Add(serviceType);
                }
            }

            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                foreach (var serviceType in eagerInitTypes)
                {
                    serviceProvider.GetService(serviceType);
                }
                return new EagerInitService();
            });
            return services;
        }
    }
}

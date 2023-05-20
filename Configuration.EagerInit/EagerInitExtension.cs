using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zack.Commons;

namespace Configuration.EagerInit
{
    public static class EagerInitExtension
    {
        public static IServiceCollection UseEagerInit(this IServiceCollection services)
        {
            Dictionary<ServiceLifetime, List<Type>> dict = new Dictionary<ServiceLifetime, List<Type>>();
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsAbstract)
                    {
                        continue;
                    }
                    var attribute = type.GetCustomAttribute<EagerInitAttribute>();
                    if (attribute == null)
                    {
                        continue;
                    }
                    services.Add(new ServiceDescriptor(type, type, attribute.ServiceLifetime));
                    dict.TryGetValue(attribute.ServiceLifetime, out var list);
                    if (list == null)
                    {
                        list = new List<Type>();
                        dict[attribute.ServiceLifetime] = list;
                    }
                    list.Add(type);
                }
            }

            services.AddTransient<IHostedService>(serviceProvider =>
            {
                IServiceScope serviceScope = serviceProvider.CreateScope();
                foreach (var keyValuePair in dict)
                {
                    if (keyValuePair.Key == ServiceLifetime.Scoped)
                    {
                        foreach (var type in keyValuePair.Value)
                        {
                            serviceScope.ServiceProvider.GetService(type);
                        }
                    }
                    else
                    {
                        foreach (var type in keyValuePair.Value)
                        {
                            serviceProvider.GetService(type);
                        }
                    }
                }
                return new EagerInitService();
            });
            return services;
        }
    }
}

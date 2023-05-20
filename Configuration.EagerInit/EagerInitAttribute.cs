using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.EagerInit
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class EagerInitAttribute : System.Attribute
    {
        public EagerInitAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Transient) { 
            ServiceLifetime = serviceLifetime;
        }
        public ServiceLifetime ServiceLifetime { get; set; }
    }
}

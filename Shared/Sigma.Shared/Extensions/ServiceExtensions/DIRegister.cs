using Sigma.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Extensions.ServiceExtensions;

public static class DIRegister
{
    public static IServiceCollection AddGenericDI(this IServiceCollection services, Assembly assembly)
    {
        if (assembly == null)
            throw new ArgumentNullException(nameof(assembly));

         var implementedServices = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IBaseService).IsAssignableFrom(t))
            .ToList();

        foreach (var implementedService in implementedServices)
        {
            var implementedServiceInterfaces = implementedService.GetInterfaces()
                .Where(i => i != typeof(IBaseService) && i != typeof(ITransientService) &&
                    i != typeof(IScopedService) && i != typeof(ISingletonService)).ToList();
            
            foreach (var implementedServiceInterface in implementedServiceInterfaces)
            {
                if (typeof(ITransientService).IsAssignableFrom(implementedServiceInterface))
                    services.AddTransient(implementedServiceInterface, implementedService);
                else if (typeof(IScopedService).IsAssignableFrom(implementedServiceInterface))
                    services.AddScoped(implementedServiceInterface, implementedService);
                else if (typeof(ISingletonService).IsAssignableFrom(implementedServiceInterface))
                    services.AddSingleton(implementedServiceInterface, implementedService);
            }
        }

        return services;
    }
}

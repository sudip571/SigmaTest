using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Sigma.Shared.PipelineBehaviours;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Sigma.Shared.Extensions.ServiceExtensions;

public static class CommonServiceRegister
{   
    public static IServiceCollection AddCommonServiceTogether(this IServiceCollection services, IConfiguration configuration, Assembly assembly = null)
    {
        services.AddApiVersion();
        services.AddCORS(configuration);
        services.AddMemoryCache();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.WriteIndented = true;
                x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            
        services.AddHttpContextAccessor();       
        services.AddFluentValidationRulesToSwagger();
        services.AddResponseCaching();
        services.AddResponseCompressions();

        if (assembly is not null)
        {
            services.AddGenericDI(assembly);            
            services.AddMediatR(config => { config.RegisterServicesFromAssembly(assembly); });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(assembly);
        }
        
        return services;
    }
}

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Sigma.Shared.Extensions.ServiceExtensions;

public static class ApiVersionRegister
{
    public static IServiceCollection AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {           
            config.DefaultApiVersion = new ApiVersion(1, 0);          
            config.AssumeDefaultVersionWhenUnspecified = true;         
            config.ReportApiVersions = true;
            config.ApiVersionReader = new HeaderApiVersionReader("api-version");
        }).AddApiExplorer(option =>
        {
            option.GroupNameFormat = "'v'VVV";
            option.SubstituteApiVersionInUrl = true;
        });
        
        return services;
    }
}

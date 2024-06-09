using Sigma.Shared.Extensions.ServiceExtensions;
using System.Reflection;

namespace Sigma.API.Extensions;

public static class SwaggerRegister
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {           
            c.AddSwaggerGenerator(configuration);

            //c.AddAppleBusinessSwaggerDoc(); //Apple Business API
           
            var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.CustomSchemaIds(name => name.FullName?.Replace("+", "."));
        });
        return services;
    }
}
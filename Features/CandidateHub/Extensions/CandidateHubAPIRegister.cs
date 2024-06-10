using System.Reflection;
using CandidateHub.Database.Context;
using Microsoft.EntityFrameworkCore;
using Sigma.Shared.Constants;
using Sigma.Shared.Extensions.ServiceExtensions;
using Sigma.Shared.Helpers;

namespace CandidateHub.Extensions;


public static class CandidateHubAPIRegister
{
    public static IServiceCollection AddCandidateHubAPIRegister(this IServiceCollection services, IConfiguration configuration, ConfigureHostBuilder host, Assembly assembly = null)
    {
        host.AddSerilogs();

        //services.AddStronglyTypedConfig(configuration);

        if (assembly is null)
            assembly = Assembly.GetExecutingAssembly();
        services.AddCommonServiceTogether(configuration, assembly);
        
        services.AddDbContext<SigmaContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(AppConstants.Sigma_Connection_String_MS),
            providerOption =>
            {
                providerOption.CommandTimeout(300);
            });

            if (configuration.GetValue<bool>("LinqQueryLog:EnableLoggingOnDevelopment"))
            {
                options.EnableSensitiveDataLogging();
                options.AddInterceptors(new SlowQueryDetectionHelper());
            }
            if (configuration.GetValue<bool>("LinqQueryLog:EnableLoggingOnProduction"))
            {
                options.EnableSensitiveDataLogging();
                options.AddInterceptors(new SlowQueryDetectionHelper());
            }
        });

        return services;

      
    }
}

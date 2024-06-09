using Microsoft.Extensions.DependencyInjection;
using Sigma.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Extensions.ServiceExtensions
{
    public static class CORSRegister
    {
        public static IServiceCollection AddCORS(this IServiceCollection services, IConfiguration configuration)
        {           

            var origins = configuration.GetSection(AppConstants.AllowedCORSOrigin).Get<List<string>>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: AppConstants.CorsPolicy, builder =>
                {                    
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins(origins.ToArray())
                    .WithExposedHeaders(AppConstants.Content_Disposition);
                });
            });

            return services;
        }
    }
}

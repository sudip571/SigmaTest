using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;


namespace Sigma.Shared.Extensions.ServiceExtensions;

public static class ResponseCompressionRegister
{
    public static IServiceCollection AddResponseCompressions(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true; 
            options.Providers.Add<BrotliCompressionProvider>(); 
            options.Providers.Add<GzipCompressionProvider>(); 
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest; 
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal; 
        });
        return services;
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using Serilog;
using Sigma.Shared.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Extensions.ServiceExtensions;


public static class MiddlewareServceRegister
{   
    public static WebApplication AddOtherWebRequest(this WebApplication app)
    {        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        if (app.Environment.IsDevelopment())
        {
            app.UseSerilogRequestLogging();
        }
        app.UseRouting();
        app.UseCors(AppConstants.CorsPolicy);

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseResponseCaching();
        app.UseResponseCompression();
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        return app;
    }
}

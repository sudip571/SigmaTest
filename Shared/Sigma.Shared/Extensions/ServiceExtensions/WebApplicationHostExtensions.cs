using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Shared.Extensions.ServiceExtensions;

public static class WebApplicationHostExtensions
{
    public static void AddSerilogs(this ConfigureHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
        host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
    }
}

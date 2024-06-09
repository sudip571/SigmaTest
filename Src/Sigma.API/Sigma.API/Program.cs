
using System.Reflection;
using Sigma.API.Extensions;
using Sigma.Shared.Extensions.ServiceExtensions;
using CandidateHub.Extensions;

namespace Sigma.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var env = builder.Environment;
            var host = builder.Host;
            var configuration = builder.Configuration;
            var assembly = Assembly.GetExecutingAssembly();

            configuration.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true);

            host.AddSerilogs();

            services.AddSwagger(configuration);
            services.AddCommonServiceTogether(configuration, assembly);

            /*Register Each Feature API project and class library service here */
            services.AddCandidateHubAPIRegister(configuration, host);


            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI(u =>
            {
                u.AddCandidateHubSwaggerUI(); 
            });

            app.AddOtherWebRequest();
            app.MapControllers();           
            app.Run();
        }
    }
}


using CandidateHub.Extensions;
using System.Reflection;

namespace CandidateHub;

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

        // Add services to the container.

        services.AddCandidateHubAPIRegister(configuration, host, assembly);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {           
            c.AddSwaggerGenerator(configuration);
            c.CustomSchemaIds(name => name.FullName);
            c.AddCandidateHubSwaggerDoc();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(u =>
            {
                u.AddCandidateHubSwaggerUI();
            });
        }

        app.AddOtherWebRequest();

        app.MapControllers();

        app.Run();
    }
}

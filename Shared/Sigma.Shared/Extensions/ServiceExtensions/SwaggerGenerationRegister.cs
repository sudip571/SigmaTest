using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sigma.Shared.Extensions.ServiceExtensions;


public static class SwaggerGenerationRegister
{
    

    public static SwaggerGenOptions AddSwaggerGenerator(this SwaggerGenOptions options, IConfiguration configuration)
    {        
        options.EnableAnnotations(); 
       
        var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
        xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

        return options;
    }
}

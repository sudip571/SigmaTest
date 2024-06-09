using CandidateHub.Helpers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CandidateHub.Extensions;

public static class CandidateHubSwaggerRegister
{
    public static SwaggerGenOptions AddCandidateHubSwaggerDoc(this SwaggerGenOptions options)
    {
        
        options.CustomSchemaIds(x => x.FullName);

        options.SwaggerDoc(SwaggerHelper.V1.CandidateHubAPIGroup, new OpenApiInfo
        {
            Version = SwaggerHelper.V1.Version,
            Title = SwaggerHelper.V1.Title,
            Description = SwaggerHelper.V1.Description,
            TermsOfService = new Uri(SwaggerHelper.V1.Terms),
            Contact = new OpenApiContact
            {
                Name = SwaggerHelper.Contact.Name,
                Email = SwaggerHelper.Contact.Email,
                Url = new Uri(SwaggerHelper.Contact.Url),
            },
            License = new OpenApiLicense
            {
                Name = SwaggerHelper.License.Name,
                Url = new Uri(SwaggerHelper.License.Url),
            },
        });
        return options;
    }

    public static SwaggerUIOptions AddCandidateHubSwaggerUI(this SwaggerUIOptions options)
    {
        options.SwaggerEndpoint($"/swagger/{SwaggerHelper.V1.CandidateHubAPIGroup}/swagger.json", "Candidate Hub V1");

        return options;
    }
}
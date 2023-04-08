using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace SantoshGantiHomeLab.Configurations;

public class MyOpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
    public override OpenApiInfo Info { get; set; } = new()
    {
        Version = GetOpenApiDocVersion(),
        Title = GetOpenApiDocTitle(),
        Description = GetOpenApiDocDescription(),
        TermsOfService = new Uri("https://github.com/Azure/azure-functions-openapi-extension"),
        Contact = new OpenApiContact
        {
            Name = "Santosh Ganti",
            Email = "admin@homelab.santoshganti.net",
            Url = new Uri("https://www.homelab.santoshganti.net")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("http://opensource.org/licenses/MIT")
        }
    };

    public override OpenApiVersionType OpenApiVersion { get; set; } = GetOpenApiVersion();
}
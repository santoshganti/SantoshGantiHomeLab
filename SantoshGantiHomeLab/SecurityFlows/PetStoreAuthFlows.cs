using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.OpenApi.Models;

namespace SantoshGantiHomeLab.SecurityFlows;

public class PetStoreAuth : OpenApiOAuthSecurityFlows
{
    public PetStoreAuth()
    {
        Implicit = new OpenApiOAuthFlow
        {
            AuthorizationUrl = new Uri("http://petstore.swagger.io/oauth/dialog"),
            Scopes = { { "write:pets", "modify pets in your account" }, { "read:pets", "read your pets" } }
        };
    }
}
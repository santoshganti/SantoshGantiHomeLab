using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.OpenApi.Models;

namespace SantoshGantiHomeLab.Headers;

public class LoginUserResponseHeader : IOpenApiCustomResponseHeader
{
    public Dictionary<string, OpenApiHeader> Headers { get; set; } = new()
    {
        {
            "X-Rate-Limit", new OpenApiHeader
            {
                Description = "calls per hour allowed by the user",
                Schema = new OpenApiSchema
                {
                    Type = "integer",
                    Format = "int32"
                }
            }
        },
        {
            "X-Expires-After", new OpenApiHeader
            {
                Description = "date in UTC when token expires",
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Format = "date-time"
                }
            }
        }
    };
}
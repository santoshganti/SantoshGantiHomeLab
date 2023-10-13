using AutoFixture;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Configurations;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SantoshGantiHomeLab.Headers;
using SantoshGantiHomeLab.Models;
using System.Net;

namespace SantoshGantiHomeLab;

public class UserHttpTrigger
{
    private readonly Fixture _fixture;
    private readonly ILogger _logger;
    private readonly OpenApiSettings _openapi;

    public UserHttpTrigger(ILoggerFactory loggerFactory, OpenApiSettings openapi, Fixture fixture)
    {
        _logger = loggerFactory.ThrowIfNullOrDefault().CreateLogger<PetHttpTrigger>();
        _openapi = openapi.ThrowIfNullOrDefault();
        _fixture = fixture.ThrowIfNullOrDefault();
    }

    [Function(nameof(CreateUser))]
    [OpenApiOperation("createUser", new[] { "user" }, Summary = "Creates user",
        Description = "This can only be done by the logged in user.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(User), Required = true, Description = "Created user object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(User), Summary = "successful operation",
        Description = "successful operation")]
    public async Task<HttpResponseData> CreateUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "user")]
        HttpRequestData req)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_fixture.Create<User>()).ConfigureAwait(false);

        return await Task.FromResult(response).ConfigureAwait(false);
    }

    [Function(nameof(CreateUsersWithArrayInput))]
    [OpenApiOperation("createUsersWithArrayInput", new[] { "user" },
        Summary = "Creates list of users with given input array",
        Description = "This Creates list of users with given input array.",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(List<User>), Required = true, Description = "List of user object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(List<User>),
        Summary = "successful operation", Description = "successful operation")]
    public async Task<HttpResponseData> CreateUsersWithArrayInput(
        [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "user/createWithArray")]
        HttpRequestData req)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_fixture.Create<List<User>>()).ConfigureAwait(false);

        return await Task.FromResult(response).ConfigureAwait(false);
    }

    [Function(nameof(CreateUsersWithListInput))]
    [OpenApiOperation("createUsersWithListInput", new[] { "user" },
        Summary = "Creates list of users with given input array",
        Description = "This Creates list of users with given input array.",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(List<User>), Required = true, Description = "List of user object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(List<User>),
        Summary = "successful operation", Description = "successful operation")]
    public async Task<HttpResponseData> CreateUsersWithListInput(
        [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "user/createWithList")]
        HttpRequestData req)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_fixture.Create<List<User>>()).ConfigureAwait(false);

        return await Task.FromResult(response).ConfigureAwait(false);
    }

    [Function(nameof(LoginUser))]
    [OpenApiOperation("loginUser", new[] { "user" }, Summary = "Logs user into the system",
        Description = "This logs user into the system.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter("username", In = ParameterLocation.Query, Required = true, Type = typeof(string),
        Summary = "The user name for login", Description = "The user name for login",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter("password", In = ParameterLocation.Query, Required = true, Type = typeof(string),
        Summary = "The password for login in clear text", Description = "The password for login in clear text",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "text/plain", typeof(string),
        CustomHeaderType = typeof(LoginUserResponseHeader), Summary = "successful operation",
        Description = "successful operation")]
    public async Task<HttpResponseData> LoginUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "user/login")]
        HttpRequestData req)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.Headers.Add("X-Rate-Limit", _fixture.Create<int>().ToString());
        response.Headers.Add("X-Expires-After",
            _fixture.Create<DateTimeOffset>().ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ"));

        await response.WriteStringAsync(_fixture.Create<string>()).ConfigureAwait(false);

        return await Task.FromResult(response).ConfigureAwait(false);
    }

    [Function(nameof(LogoutUser))]
    [OpenApiOperation("logoutUser", new[] { "user" }, Summary = "Logs out current logged in user session",
        Description = "This logs out current logged in user session.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK, Summary = "successful operation",
        Description = "successful operation")]
    public async Task<HttpResponseData> LogoutUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "user/logout")]
        HttpRequestData req)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);

        return await Task.FromResult(response).ConfigureAwait(false);
    }

    [Function(nameof(GetUserByName))]
    [OpenApiOperation("getUserByName", new[] { "user" }, Summary = "Gets user by user name",
        Description = "This gets user by user name.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter("username", In = ParameterLocation.Path, Required = true, Type = typeof(string),
        Summary = "The user name for login", Description = "The user name for login",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(User), Summary = "successful operation",
        Description = "successful operation")]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest, Summary = "Invalid username supplied",
        Description = "Invalid username supplied")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Summary = "User not found", Description = "User not found")]
    public async Task<HttpResponseData> GetUserByName(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "user/{username:regex((?!^login$)(^.+$))}")]
        HttpRequestData req, string username)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);
        var user = _fixture.Build<User>().With(p => p.Username, username).Create();
        await response.WriteAsJsonAsync(user).ConfigureAwait(false);

        return await Task.FromResult(response).ConfigureAwait(false);
    }
}
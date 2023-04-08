using System.Net;
using AutoFixture;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Configurations;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SantoshGantiHomeLab.Models;

namespace SantoshGantiHomeLab;

public class StoreHttpTrigger
{
    private readonly Fixture _fixture;
    private readonly ILogger _logger;
    private readonly OpenApiSettings _openapi;

    public StoreHttpTrigger(ILoggerFactory loggerFactory, OpenApiSettings openapi, Fixture fixture)
    {
        _logger = loggerFactory.ThrowIfNullOrDefault().CreateLogger<PetHttpTrigger>();
        _openapi = openapi.ThrowIfNullOrDefault();
        _fixture = fixture.ThrowIfNullOrDefault();
    }

    [Function(nameof(GetInventory))]
    [OpenApiOperation("getInventory", new[] { "store" }, Summary = "Returns pet inventories by status",
        Description = "This returns a map of status codes to quantities.",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiSecurity("api_key", SecuritySchemeType.ApiKey, Name = "api_key", In = OpenApiSecurityLocationType.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Dictionary<string, int>),
        Description = "Successful operation")]
    public async Task<HttpResponseData> GetInventory(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "store/inventory")]
        HttpRequestData req)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);
        var result = _fixture.Create<Dictionary<string, int>>();
        await response.WriteAsJsonAsync(result).ConfigureAwait(false);

        return await Task.FromResult(response).ConfigureAwait(false);
    }

    [Function(nameof(PlaceOrder))]
    [OpenApiOperation("placeOrder", new[] { "store" }, Summary = "Places an order for a pet",
        Description = "This places an order for a pet.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody("application/json", typeof(Order), Required = true,
        Description = "Order placed for purchasing the pet")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Order), Summary = "successful operation",
        Description = "successful operation")]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest, Summary = "Invalid input", Description = "Invalid input")]
    public async Task<HttpResponseData> PlaceOrder(
        [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "store/order")]
        HttpRequestData req)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_fixture.Create<Order>()).ConfigureAwait(false);

        return await Task.FromResult(response).ConfigureAwait(false);
    }

    [Function(nameof(GetOrderById))]
    [OpenApiOperation("getOrderById", new[] { "store" }, Summary = "Finds purchase order by ID",
        Description = "This finds purchase order by ID.", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter("orderId", In = ParameterLocation.Path, Required = true, Type = typeof(long),
        Summary = "ID of order that needs to be fetched", Description = "ID of order that needs to be fetched",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(Order),
        Description = "Successful operation")]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest, Summary = "Invalid ID supplied",
        Description = "Invalid ID supplied")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Summary = "Order not found", Description = "Order not found")]
    public async Task<HttpResponseData> GetOrderById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "store/order/{orderId}")]
        HttpRequestData req, long orderId)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);
        var order = _fixture.Build<Order>().With(p => p.Id, orderId).Create();
        await response.WriteAsJsonAsync(order).ConfigureAwait(false);

        return await Task.FromResult(response).ConfigureAwait(false);
    }

    [Function(nameof(DeleteOrder))]
    [OpenApiOperation("deleteOrder", new[] { "store" }, Summary = "Deletes purchase order by ID",
        Description =
            "For valid response try integer IDs with positive integer value. Negative or non - integer values will generate API errors.",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter("orderId", In = ParameterLocation.Path, Required = true, Type = typeof(long),
        Summary = "ID of order that needs to be deleted", Description = "ID of order that needs to be deleted",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest, Summary = "Invalid ID supplied",
        Description = "Invalid ID supplied")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Summary = "Order not found", Description = "Order not found")]
    public async Task<HttpResponseData> DeleteOrder(
        [HttpTrigger(AuthorizationLevel.Anonymous, "DELETE", Route = "store/order/{orderId}")]
        HttpRequestData req, long orderId)
    {
        _logger.LogInformation($"document title: {_openapi.DocTitle}");
        var response = req.CreateResponse(HttpStatusCode.OK);

        return await Task.FromResult(response).ConfigureAwait(false);
    }
}
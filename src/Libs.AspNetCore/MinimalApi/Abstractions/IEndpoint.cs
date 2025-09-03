using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.MinimalApi.Abstractions;

public interface IEndpoint
{
    RouteHandlerBuilder Map(IEndpointRouteBuilder builder);
}
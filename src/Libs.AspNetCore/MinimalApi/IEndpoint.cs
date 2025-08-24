using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.MinimalApi;

public interface IEndpoint
{
    void Map(IEndpointRouteBuilder builder);
}
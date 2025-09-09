using FwksLabs.Libs.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class DeferredResultHandlingConfiguration
{
    public static IServiceCollection AddDeferredResultHandling(this IServiceCollection services) =>
        services
            .AddScoped<DeferredResultHandlingMiddleware>();

    public static IApplicationBuilder UseDeferredResultHandling(this IApplicationBuilder builder) =>
        builder
            .UseMiddleware<DeferredResultHandlingMiddleware>();
}
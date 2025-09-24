using FwksLabs.Libs.AspNetCore.Middlewares;
using FwksLabs.Libs.Core.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class MiddlewareConfiguration
{
    public static IServiceCollection AddCorrelationContext(this IServiceCollection services) =>
        services
            .AddScoped<CorrelationContext>();

    public static IApplicationBuilder UseCorrelationContext(this IApplicationBuilder builder) =>
        builder
            .UseMiddleware<CorrelationMiddleware>();

    public static IApplicationBuilder UseRequestEnrichment(this IApplicationBuilder builder) =>
        builder
            .UseMiddleware<RequestEnrichmentMiddleware>();

    public static IApplicationBuilder UseDeferredResultHandling(this IApplicationBuilder builder) =>
        builder
            .UseMiddleware<DeferredResultHandlingMiddleware>();
}
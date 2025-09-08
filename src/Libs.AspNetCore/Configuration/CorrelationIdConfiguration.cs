using FwksLabs.Libs.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class CorrelationIdConfiguration
{
    public static IServiceCollection AddCorrelationId(this IServiceCollection services)
    {
        return services
            .AddScoped<CorrelationIdMiddleware>();
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
    {
        return builder
            .UseMiddleware<CorrelationIdMiddleware>();
    }
}
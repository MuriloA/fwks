using FwksLabs.Libs.AspNetCore.Middlewares;
using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class CorrelationIdConfiguration
{
    public static IServiceCollection AddCorrelationId(this IServiceCollection services) =>
        services
            .AddScoped<ICorrelationIdContext, CorrelationIdContext>()
            .AddScoped<CorrelationIdMiddleware>();

    public static IServiceCollection AddCorrelationId<TCorrelationContext>(this IServiceCollection services)
        where TCorrelationContext : class, ICorrelationIdContext =>
        services
            .AddScoped<ICorrelationIdContext, TCorrelationContext>()
            .AddScoped<CorrelationIdMiddleware>();

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder) =>
        builder
            .UseMiddleware<CorrelationIdMiddleware>();
}
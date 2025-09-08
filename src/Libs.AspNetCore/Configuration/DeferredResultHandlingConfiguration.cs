using FwksLabs.Libs.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class DeferredResultHandlingConfiguration
{
    public static IServiceCollection AddDeferredResultHandling(this IServiceCollection services)
    {
        return services
            .AddScoped<DeferredResultHandlingMiddleware>();
    }
    
    public static IApplicationBuilder UseDeferredResultHandling(this IApplicationBuilder builder)
    {
        return builder
            .UseMiddleware<DeferredResultHandlingMiddleware>();
    }
}
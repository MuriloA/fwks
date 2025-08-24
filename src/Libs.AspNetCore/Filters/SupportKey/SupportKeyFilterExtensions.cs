using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Filters.SupportKey;

public static class SupportKeyFilterExtensions
{
    public static IServiceCollection AddSupportKeyFilter(this IServiceCollection services, Action<SupportKeyFilterOptions> setupAction)
    {
        return services.Configure(setupAction);
    }

    public static RouteHandlerBuilder WithSupportKeyFilter(this RouteHandlerBuilder builder)
    {
        return builder.AddEndpointFilter<SupportKeyFilter>();
    }
}
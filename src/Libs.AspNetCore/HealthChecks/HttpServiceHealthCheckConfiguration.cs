using System;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.HealthChecks;

public static class HttpServiceHealthCheckConfiguration
{
    public static IServiceCollection AddHttpServiceHealthCheck(this IServiceCollection services, string name, string url, int timeoutInSeconds, string[] tags)
    {
        name = name.Kebaberize();
        var timeout = TimeSpan.FromSeconds(timeoutInSeconds);

        services.AddHttpClient($"healthechecks-{name}", client =>
        {
            client.DefaultRequestHeaders.Add(AppHealthCheckProperties.Headers.Client, name);
            client.BaseAddress = new Uri(url);
            client.Timeout = timeout;
        });

        var failureStatus = tags.Contains(AppHealthCheckProperties.Tags.Critical) ? HealthStatus.Unhealthy : HealthStatus.Degraded;

        tags = [AppHealthCheckProperties.Tags.Readiness, AppHealthCheckProperties.Tags.TypeHttpService, .. tags];

        services
            .AddHealthChecks()
            .AddCheck<HttpServiceHealthCheck>(name, failureStatus, [.. tags.Select(x => x.ToLowerInvariant())], timeout);

        return services;
    }
}
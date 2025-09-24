using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.HealthChecks;

public static class HttpServiceHealthCheckConfiguration
{
    public static IServiceCollection AddHttpServiceHealthCheck(this IServiceCollection services, string name, string url, int timeoutInSeconds,
        Dictionary<string, IEnumerable<string>> requestHeaders, string[] tags)
    {
        name = name.Kebaberize();
        var timeout = TimeSpan.FromSeconds(timeoutInSeconds);

        services.AddHttpClient($"health-checks-{name}", client =>
        {
            client.DefaultRequestHeaders.Add(HealthCheckProperties.Headers.Client, name);
            client.BaseAddress = new Uri(url);
            client.Timeout = timeout;

            foreach (var header in requestHeaders)
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
        });

        var failureStatus = tags.Contains(HealthCheckProperties.Tags.Critical) ? HealthStatus.Unhealthy : HealthStatus.Degraded;

        tags = [HealthCheckProperties.Tags.Readiness, HealthCheckProperties.Tags.TypeHttpService, .. tags];

        services
            .AddHealthChecks()
            .AddCheck<HttpServiceHealthCheck>(name, failureStatus, [.. tags.Select(x => x.ToLowerInvariant())], timeout);

        return services;
    }
}
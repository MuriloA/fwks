using System.Collections.Generic;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.HealthChecks;

public static class HealthCheckConfiguration
{
    private static readonly Dictionary<HealthStatus, int> ResultStatusCodes = new()
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    };

    public static IApplicationBuilder UseHealthCheckEndpoints(this IApplicationBuilder app) =>
        app
            .AddLivenessEndpoint()
            .AddReadinessEndpoint();

    private static IApplicationBuilder AddLivenessEndpoint(this IApplicationBuilder app)
    {
        return app
            .UseHealthChecks(AppHealthCheckProperties.Endpoints.Liveness, new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(AppHealthCheckProperties.Tags.Liveness),
                AllowCachingResponses = false,
                ResultStatusCodes = ResultStatusCodes,
                ResponseWriter = async (context, report) => await context.Response.WriteAsJsonAsync(new { report.Status })
            });
    }

    private static IApplicationBuilder AddReadinessEndpoint(this IApplicationBuilder app)
    {
        return app
            .UseHealthChecks(AppHealthCheckProperties.Endpoints.Readiness, new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(AppHealthCheckProperties.Tags.Readiness),
                AllowCachingResponses = false,
                ResultStatusCodes = ResultStatusCodes,
                ResponseWriter = async (context, report) => await context.Response.WriteAsJsonAsync(HealthCheckReadinessReport.From(report))
            });
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.HealthChecks;

public sealed record HealthCheckReadinessReport
{
    public required HealthStatus Status { get; init; }
    public required TimeSpan TotalDuration { get; init; }
    public IEnumerable<HealthCheckDependencyReport> Dependencies { get; init; } = [];


    public static HealthCheckReadinessReport From(HealthReport report)
    {
        return new HealthCheckReadinessReport
        {
            Status = GetStatus(report),
            TotalDuration = report.TotalDuration,
            Dependencies = [.. report.Entries.Select(HealthCheckDependencyReport.From)]
        };
    }

    private static HealthStatus GetStatus(HealthReport report)
    {
        var status = report.Status;

        if (status == HealthStatus.Unhealthy)
            status = HasCriticalUnhealthy() ? HealthStatus.Unhealthy : HealthStatus.Degraded;

        return status;

        bool HasCriticalUnhealthy()
        {
            return report.Entries.Any(x =>
                x.Value.Tags.Contains(HealthCheckProperties.Tags.Critical) &&
                x.Value.Status is not HealthStatus.Healthy);
        }
    }
}
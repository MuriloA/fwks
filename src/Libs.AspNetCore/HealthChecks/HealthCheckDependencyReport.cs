using System;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.HealthChecks;

public sealed record HealthCheckDependencyReport
{
    public required string Name { get; init; }
    public required HealthStatus Status { get; init; }
    public required TimeSpan Duration { get; init; }
    public required string[] Tags { get; init; }

    public static HealthCheckDependencyReport From(KeyValuePair<string, HealthReportEntry> entry)
    {
        return new HealthCheckDependencyReport
        {
            Name = entry.Key,
            Status = entry.Value.Status,
            Duration = entry.Value.Duration,
            Tags = [.. entry.Value.Tags]
        };
    }
}
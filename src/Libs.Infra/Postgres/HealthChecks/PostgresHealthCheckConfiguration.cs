using System;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace FwksLabs.Libs.Infra.Postgres.HealthChecks;

public static class PostgresHealthCheckConfiguration
{
    public static IServiceCollection AddPostgresHealthCheck(this IServiceCollection services, string connString)
    {
        string[] tags = [HealthCheckProperties.Tags.Readiness, HealthCheckProperties.Tags.TypeDatabase, HealthCheckProperties.Tags.Critical];

        services
            .AddKeyedTransient<NpgsqlDataSource>("health-checks-postgres", (_, _) => NpgsqlDataSource.Create(connString))
            .AddHealthChecks()
            .AddCheck<PostgresHealthCheck>("postgres", HealthStatus.Unhealthy, tags, TimeSpan.FromSeconds(5));

        return services;
    }
}
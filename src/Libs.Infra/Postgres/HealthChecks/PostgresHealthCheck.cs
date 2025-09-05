using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace FwksLabs.Libs.Infra.Postgres.HealthChecks;

public sealed class PostgresHealthCheck(
    ILogger<PostgresHealthCheck> logger,
    IServiceProvider serviceProvider) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        try
        {
            await using var dataSource = serviceProvider.GetRequiredKeyedService<NpgsqlDataSource>($"healthechecks-{context.Registration.Name}");

            var connection = dataSource.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1;";

            var result = await command.ExecuteScalarAsync(cancellationToken);

            return result is 1
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy("PostgreSQL database is unavailable or unreachable.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while trying to reach postgres instance.");

            return new HealthCheckResult(context.Registration.FailureStatus, "An error occurred while trying to connect to the PostgreSQL isntance.", ex);
        }
    }
}
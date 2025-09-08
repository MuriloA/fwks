using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace FwksLabs.Libs.AspNetCore.HealthChecks;

public sealed class HttpServiceHealthCheck(
    ILogger<HttpServiceHealthCheck> logger,
    IHttpClientFactory clientFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = clientFactory.CreateClient($"healthechecks-{context.Registration.Name}");

            var response = await httpClient.GetAsync(string.Empty, cancellationToken);

            return response.IsSuccessStatusCode ? HealthCheckResult.Healthy() : Failure();
        }
        catch (Exception ex)
        {
            return Failure(ex);
        }

        HealthCheckResult Failure(Exception? ex = null)
        {
            logger.LogError(ex, "Error while trying to reach service {ServiceName}.", context.Registration.Name);

            return new HealthCheckResult(context.Registration.FailureStatus, CommonErrors.BadGateway.Detail, ex);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace FwksLabs.Libs.Infra.Postgres.Resilience;

public static class ResilienceExtensions
{
    private static readonly HashSet<HttpStatusCode> StatusCodes =
    [
        HttpStatusCode.RequestTimeout,
        HttpStatusCode.TooManyRequests,
        HttpStatusCode.InternalServerError,
        HttpStatusCode.BadGateway,
        HttpStatusCode.ServiceUnavailable,
        HttpStatusCode.GatewayTimeout
    ];
    
    public static ResiliencePipelineBuilder<HttpResponseMessage> AddTimeout(this ResiliencePipelineBuilder<HttpResponseMessage> builder, string name,
        Action<HttpTimeoutStrategyOptions>? optionsAction = null)
    {
        HttpTimeoutStrategyOptions options = new()
        {
            Name = name,
            Timeout = TimeSpan.FromSeconds(5)
        };

        optionsAction?.Invoke(options);

        return builder.AddTimeout(options);
    }

    public static ResiliencePipelineBuilder<HttpResponseMessage> AddCircuitBreaker(this ResiliencePipelineBuilder<HttpResponseMessage> builder, string name,
        Action<CircuitBreakerStrategyOptions<HttpResponseMessage>>? optionsAction = null)
    {
        CircuitBreakerStrategyOptions<HttpResponseMessage> options = new()
        {
            Name = name,
            FailureRatio = 0.25,
            MinimumThroughput = 10,
            SamplingDuration = TimeSpan.FromSeconds(60),
            BreakDuration = TimeSpan.FromSeconds(30),
            ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                .HandleResult(response => StatusCodes.Contains(response.StatusCode))
        };

        optionsAction?.Invoke(options);

        return builder.AddCircuitBreaker(options);
    }

    public static ResiliencePipelineBuilder<HttpResponseMessage> AddRetry(this ResiliencePipelineBuilder<HttpResponseMessage> builder, string name,
        Action<RetryStrategyOptions<HttpResponseMessage>>? optionsAction = null)
    {
        RetryStrategyOptions<HttpResponseMessage> options = new()
        {
            Name = name,
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromMilliseconds(500),
            BackoffType = DelayBackoffType.Exponential,
            ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                .HandleResult(response => StatusCodes.Contains(response.StatusCode))
        };

        optionsAction?.Invoke(options);

        return builder.AddRetry(options);
    }
}
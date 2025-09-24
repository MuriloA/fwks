using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Extensions;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Context;
using Standard = FwksLabs.Libs.Core.OpenTelemetry.OpenTelemetryProperties.Standard;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class CorrelationMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context)
    {
        var correlationContext = context.RequestServices.GetRequiredService<CorrelationContext>();

        var correlationId = context.GetRequestHeader(CommonHeaders.CorrelationId, Guid.NewGuid().ToString("D"));
        var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;
        var spanId = Activity.Current?.SpanId.ToString();

        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CommonHeaders.CorrelationId] = correlationId;
            return Task.CompletedTask;
        });

        correlationContext.Update(correlationId, traceId, spanId);

        using (LogContext.PushProperty(Standard.CorrelationId, correlationId))
        using (LogContext.PushProperty(Standard.TraceId, traceId))
        using (LogContext.PushProperty(Standard.SpanId, spanId))
        {
            return next(context);
        }
    }
}
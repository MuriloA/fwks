using System;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Abstractions;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class CorrelationIdMiddleware(
    ICorrelationIdContext correlationIdContext) : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = context.Request.Headers.TryGetValue(CommonHeaders.CorrelationId, out var value)
            ? value.ToString()
            : Guid.NewGuid().ToString();

        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Append(CommonHeaders.CorrelationId, correlationId);

            return Task.CompletedTask;
        });

        correlationIdContext.SetCorrelationId(correlationId);

        using (LogContext.PushProperty(nameof(CommonHeaders.CorrelationId), correlationId))
        {
            return next(context);
        }
    }
}
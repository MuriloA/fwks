using System;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Contexts;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class CorrelationIdMiddleware(
    CorrelationContext correlationContext) : IMiddleware
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

        correlationContext.SetCorrelationId(correlationId);

        using (LogContext.PushProperty(nameof(CommonHeaders.CorrelationId), correlationId))
        {
            return next(context);
        }
    }
}
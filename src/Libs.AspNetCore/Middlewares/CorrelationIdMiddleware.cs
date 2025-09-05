using System;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Context;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class CorrelationIdMiddleware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var appContext = context.RequestServices.GetRequiredService<AppRequestContext>();

        var correlationId = context.Request.Headers.TryGetValue(AppHeaders.CorrelationId, out var value)
            ? value.ToString()
            : Guid.NewGuid().ToString();

        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Append(AppHeaders.CorrelationId, correlationId);

            return Task.CompletedTask;
        });

        appContext.SetCorrelationId(correlationId);

        using (LogContext.PushProperty(nameof(AppHeaders.CorrelationId), correlationId))
        {
            return next(context);
        }
    }
}
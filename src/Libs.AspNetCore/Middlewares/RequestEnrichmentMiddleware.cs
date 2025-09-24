using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Extensions;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Standard = FwksLabs.Libs.Core.OpenTelemetry.OpenTelemetryProperties.Standard;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class RequestEnrichmentMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty(Standard.HttpHost, context.Request.Host.Value))
        using (LogContext.PushProperty(Standard.HttpUserAgent, context.GetRequestHeader(CommonHeaders.UserAgent)))
        using (LogContext.PushProperty(Standard.NetPeerIp, context.Connection.RemoteIpAddress?.ToString() ?? "Unidentified"))
        {
            await next(context);
        }
    }
}
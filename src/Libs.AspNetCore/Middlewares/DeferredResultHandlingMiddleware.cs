using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Configuration;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class DeferredResultHandlingMiddleware(
    INotificationContext notificationContext,
    ErrorCodeConfiguration errorCodeConfiguration) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);

        if (notificationContext.Error is not null && context.Response.HasStarted is false)
        {
            var response = Responses.Problem(notificationContext.Error!, errorCodeConfiguration.Codes, notificationContext.Messages);

            await response.ExecuteAsync(context);
        }
    }
}
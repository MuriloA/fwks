using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Configuration;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class DeferredResultMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        INotificationContext notificationContext,
        ErrorCodeConfiguration errorCodeConfiguration)
    {
        await next(context);

        if (notificationContext.Error is not null && context.Response.HasStarted is false)
        {
            var response = Responses.Problem(
                notificationContext.Error!,
                errorCodeConfiguration.Codes,
                notificationContext.Messages);

            await response.ExecuteAsync(context);
        }
    }
}
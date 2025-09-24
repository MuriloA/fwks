using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Configuration;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class DeferredResultHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        var notificationContext = context.RequestServices.GetRequiredService<INotificationContext>();
        var errorCodeConfiguration = context.RequestServices.GetRequiredService<ErrorCodeConfiguration>();

        if (notificationContext.Error is not null && context.Response.HasStarted is not true)
        {
            var response = Responses.Problem(notificationContext.Error!, errorCodeConfiguration.Codes, notificationContext.Messages);
            await response.ExecuteAsync(context);
        }
    }
}
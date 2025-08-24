using System.Net;
using System.Net.Mime;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FwksLabs.Libs.AspNetCore.Exceptions.Handlers;

public static class UnexpectedErrorHandler
{
    public static IApplicationBuilder UseUnexpectedErrorHandler(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(handler => handler.Run(async context =>
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();

            if (feature is null)
                return;

            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(UnexpectedErrorHandler).FullName!);
            logger.LogError(feature.Error, "An unexpected error occurred.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.ProblemJson;

            await context.Response.WriteAsJsonAsync(AppResponses.Problem(
                HttpStatusCode.InternalServerError, "An unexpected error occurred", AppCommonErrors.UnexpectedError));
        }));
    }
}
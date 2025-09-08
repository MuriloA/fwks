using System;
using FwksLabs.Libs.AspNetCore.Middlewares;
using FwksLabs.Libs.Core.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class NotificationContextMiddlewareConfiguration
{
    public static IApplicationBuilder DeferredResultHandlingConfiguration(this IApplicationBuilder builder)
    {
        var services = builder.ApplicationServices;

        var errorConfiguration = services.GetService<ErrorCodeConfiguration>();
        var notificationContext = services.GetService<INotificationContext>();

        if (errorConfiguration is null || notificationContext is null)
            throw new InvalidOperationException(
                """
                Cannot use DeferredResultHandler middleware because required services are missing from the DI container.
                Ensure that both 'ErrorCodeConfiguration' and 'INotificationContext' are registered.
                """);

        return builder
            .UseMiddleware<DeferredResultMiddleware>();
    }
}
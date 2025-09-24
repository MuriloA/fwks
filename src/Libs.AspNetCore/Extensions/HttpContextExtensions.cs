using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Extensions;

public static class HttpContextExtensions
{
    public static T GetRequiredService<T>(this HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var service = context.RequestServices.GetService<T>();

        if (service is null)
            throw new InvalidOperationException(
                $"Required service of type '{typeof(T).FullName}' is not registered in the DI container.");

        return service;
    }

    public static T? GetService<T>(this HttpContext context) => context.RequestServices.GetService<T>();

    public static string GetRequestHeader(this HttpContext httpContext, string key, string? fallback = null) =>
        httpContext.Request.Headers.TryGetValue(key, out var headerValue) is not true
            ? fallback ?? string.Empty
            : headerValue.ToString();
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Extensions;

public static class EndpointFilterInvocationContextExtensions
{
    public static T GetRequiredService<T>(this EndpointFilterInvocationContext context)
    {
        return context.HttpContext.GetRequiredService<T>();
    }

    public static T? GetService<T>(this EndpointFilterInvocationContext context)
    {
        return context.HttpContext.RequestServices.GetService<T>();
    }
}
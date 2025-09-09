using FwksLabs.Libs.AspNetCore.Configuration;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Extensions;

public static class ErrorCodeConfigurationExtensions
{
    public static ErrorCodeConfiguration GetErrorCodeConfiguration(this EndpointFilterInvocationContext context) => context.GetRequiredService<ErrorCodeConfiguration>();

    public static ErrorCodeConfiguration GetErrorCodeConfiguration(this HttpContext context) => context.GetRequiredService<ErrorCodeConfiguration>();
}
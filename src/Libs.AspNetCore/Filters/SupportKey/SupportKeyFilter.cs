using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.AspNetCore.Extensions;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Filters.SupportKey;

public sealed class SupportKeyFilter(
    ILogger<SupportKeyFilter> logger,
    IOptions<SupportKeyFilterOptions> options) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var hasHeader = context.HttpContext.Request.Headers.TryGetValue(CommonHeaders.SupportKey, out var headerValue);

        if (hasHeader && headerValue.Equals(options.Value.Token))
            return await next(context);

        logger.LogError("Unauthorized access attempt with support key.");

        return Responses.Problem(CommonErrors.Unauthorized, context.GetErrorCodeConfiguration().Codes, null);
    }
}
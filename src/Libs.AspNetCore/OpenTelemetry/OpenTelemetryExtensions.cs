using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using OpenTelemetry.Trace;

namespace FwksLabs.Libs.AspNetCore.OpenTelemetry;

public static class OpenTelemetryExtensions
{
    public static TracerProviderBuilder AddHttpClientInstrumentationWithFilters(this TracerProviderBuilder builder,
        IEnumerable<string> pathsFilter,
        Func<HttpRequestMessage, bool>? requestFilter = null)
    {
        var excludedPaths = pathsFilter.Select(x => x.ToLowerInvariant()).Concat(["/health/"]).ToArray();
        
        return builder
            .AddHttpClientInstrumentation(options =>
            {
                options.RecordException = true;
                options.FilterHttpRequestMessage = request =>
                {
                    if (Activity.Current?.Parent is null || request.RequestUri is null)
                        return false;

                    var path = request.RequestUri.PathAndQuery.ToLowerInvariant();

                    if (excludedPaths.Any(path.StartsWith))
                        return false;

                    return requestFilter is null || requestFilter(request);
                };
            });
    }

    public static TracerProviderBuilder AddAspNetCoreInstrumentationWithFilters(this TracerProviderBuilder builder, IEnumerable<string> pathsFilter)
    {
        var excludedPaths = pathsFilter.Select(x => x.ToLowerInvariant()).Concat(["/health/"]).ToArray();
        
        return builder
            .AddAspNetCoreInstrumentation(options =>
            {
                options.RecordException = true;
                options.Filter = context =>
                {
                    if (Activity.Current?.Parent is null || context.Request.Path.Value is null)
                        return false;

                    var path = context.Request.Path.Value?.ToLowerInvariant() ?? string.Empty;

                    return !excludedPaths.Any(path.StartsWith);
                };
            });
    }
}
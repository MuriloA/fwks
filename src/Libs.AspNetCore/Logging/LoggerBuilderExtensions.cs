using Serilog;
using Serilog.Core;

namespace FwksLabs.Libs.AspNetCore.Logging;

public static class LoggerBuilderExtensions
{
    public static ILogger MinimalIfInvalid()
    {
        return Log.Logger is Logger
            ? Log.Logger
            : new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
    }
}
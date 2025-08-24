using System;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;

namespace FwksLabs.Libs.Core.OpenTelemetry;

public static class OpenTelemetrySerilogConfiguration
{
    public static LoggerConfiguration Create(OpenTelemetryLoggerOptions loggerOptions, OpenTelemetryAppInfoOptions serviceOptions)
    {
        loggerOptions.Attributes.Add(OpenTelemetryProperties.Standard.ServiceInstanceId, Environment.MachineName);
        loggerOptions.Attributes.Add(OpenTelemetryProperties.Standard.ServiceName, serviceOptions.Name.Kebaberize());
        loggerOptions.Attributes.Add(OpenTelemetryProperties.Standard.ServiceVersion, serviceOptions.Version);
        
        loggerOptions.Attributes.Add(OpenTelemetryProperties.FwksLabs.ServicePlatform, serviceOptions.Platform.Kebaberize());
        loggerOptions.Attributes.Add(OpenTelemetryProperties.FwksLabs.ServiceNamespace, serviceOptions.Namespace.Kebaberize());
        loggerOptions.Attributes.Add(OpenTelemetryProperties.FwksLabs.ServiceMaintainer, serviceOptions.Maintainer.Kebaberize());

        var minimumLevel = loggerOptions.MinimumLevel.AsEnum<LogEventLevel>();

        var configuration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Is(minimumLevel);

        foreach (var level in loggerOptions.MinimumLevelOverrides)
            configuration.MinimumLevel.Override(level.Key, level.Value.AsEnum<LogEventLevel>());

        configuration.WriteTo.OpenTelemetry(
            endpoint: loggerOptions.CollectorEndpoint,
            protocol: OtlpProtocol.Grpc,
            restrictedToMinimumLevel: minimumLevel,
            resourceAttributes: loggerOptions.Attributes);

        return configuration;
    }
}
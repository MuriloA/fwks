using System;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;

namespace FwksLabs.Libs.Core.OpenTelemetry;

public static class OpenTelemetrySerilogConfiguration
{
    public static LoggerConfiguration Create(Action<OpenTelemetrySerilogOptions> optionsAction)
    {
        var options = new OpenTelemetrySerilogOptions();

        optionsAction.Invoke(options);

        AddAttributes();

        var minimumLevel = options.LoggerMinimumLevel.AsEnum<LogEventLevel>();

        var configuration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Is(minimumLevel);

        foreach (var level in options.LoggerMinimumLevelOverrides)
            configuration.MinimumLevel.Override(level.Key, level.Value.AsEnum<LogEventLevel>());

        configuration.WriteTo.OpenTelemetry(
            endpoint: options.LoggerCollectorEndpoint,
            protocol: OtlpProtocol.Grpc,
            restrictedToMinimumLevel: minimumLevel,
            resourceAttributes: options.Attributes);

        return configuration;

        void AddAttributes()
        {
            options.Attributes.Add(OpenTelemetryProperties.Standard.ServiceInstanceId, Environment.MachineName);
            options.Attributes.Add(OpenTelemetryProperties.Standard.ServiceName, options.AppName.Kebaberize());
            options.Attributes.Add(OpenTelemetryProperties.Standard.ServiceVersion, options.AppVersion);

            options.Attributes.Add(OpenTelemetryProperties.FwksLabs.ServicePlatform, options.AppPlatform.Kebaberize());
            options.Attributes.Add(OpenTelemetryProperties.FwksLabs.ServiceNamespace, options.AppNamespace.Kebaberize());
            options.Attributes.Add(OpenTelemetryProperties.FwksLabs.ServiceMaintainer, options.AppMaintainer.Kebaberize());
        }
    }
}
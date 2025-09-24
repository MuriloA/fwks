using System;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Standard = FwksLabs.Libs.Core.OpenTelemetry.OpenTelemetryProperties.Standard;
using Fwks = FwksLabs.Libs.Core.OpenTelemetry.OpenTelemetryProperties.FwksLabs;

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
            .Enrich.WithSpan()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .MinimumLevel.Is(minimumLevel);

        foreach (var level in options.LoggerMinimumLevelOverrides)
            configuration.MinimumLevel.Override(level.Key, level.Value.AsEnum<LogEventLevel>());

        configuration.WriteTo.OpenTelemetry(
            options.LoggerCollectorEndpoint,
            restrictedToMinimumLevel: minimumLevel,
            resourceAttributes: options.Attributes);

        return configuration;

        void AddAttributes()
        {
            options.Attributes.Add(Standard.ServiceInstanceId, Environment.MachineName);
            options.Attributes.Add(Standard.ServiceName, options.AppName.Kebaberize());
            options.Attributes.Add(Standard.ServiceVersion, options.AppVersion);

            options.Attributes.Add(Fwks.ServicePlatform, options.AppPlatform.Kebaberize());
            options.Attributes.Add(Fwks.ServiceNamespace, options.AppNamespace.Kebaberize());
            options.Attributes.Add(Fwks.ServiceMaintainer, options.AppMaintainer.Kebaberize());
        }
    }
}
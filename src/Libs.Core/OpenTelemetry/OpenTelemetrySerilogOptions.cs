using System.Collections.Generic;

namespace FwksLabs.Libs.Core.OpenTelemetry;

public sealed class OpenTelemetrySerilogOptions
{
    public string AppVersion { get; set; } = "0.0.0";
    public string AppPlatform { get; set; } = "DEFAULT_PLATFORM";
    public string AppNamespace { get; set; } = "DEFAULT_NAMESPACE";
    public string AppName { get; set; } = "DEFAULT_NAME";
    public string AppMaintainer { get; set; } = "DEFAULT_MAINTAINER";
    public string LoggerMinimumLevel { get; set; } = "Information";
    public string LoggerCollectorEndpoint { get; set; } = "http://localhost:4317";
    public Dictionary<string, string> LoggerMinimumLevelOverrides { get; set; } = [];
    public Dictionary<string, object> Attributes { get; set; } = [];
}
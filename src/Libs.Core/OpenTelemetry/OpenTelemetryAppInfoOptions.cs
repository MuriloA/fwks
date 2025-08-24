namespace FwksLabs.Libs.Core.OpenTelemetry;

public record OpenTelemetryAppInfoOptions(
    string Version,
    string Platform,
    string Namespace,
    string Name,
    string Maintainer);
using System.Collections.Generic;

namespace FwksLabs.Libs.Core.OpenTelemetry;

public record OpenTelemetryLoggerOptions(
    string MinimumLevel,
    string CollectorEndpoint,
    Dictionary<string, string> MinimumLevelOverrides,
    Dictionary<string, object> Attributes);
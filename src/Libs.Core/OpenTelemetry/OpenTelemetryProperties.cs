namespace FwksLabs.Libs.Core.OpenTelemetry;

public static class OpenTelemetryProperties
{
    public static class Endpoints
    {
        public const string Traces = "/v1/traces";
        public const string Metrics = "/v1/metrics";
    }

    public class Standard
    {
        public const string ServiceInstanceId = "service.instance.id";
        public const string ServiceName = "service.name";
        public const string ServiceVersion = "service.version";
    }

    public class FwksLabs
    {
        public const string ServicePlatform = "fl.service.platform";
        public const string ServiceNamespace = "fl.service.namespace";
        public const string ServiceMaintainer = "fl.service.maintainer";
    }
}
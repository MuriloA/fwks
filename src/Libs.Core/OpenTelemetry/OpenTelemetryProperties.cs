namespace FwksLabs.Libs.Core.OpenTelemetry;

public static class OpenTelemetryProperties
{
    public static class Endpoints
    {
        public const string Traces = "/v1/traces";
        public const string Metrics = "/v1/metrics";
    }

    public static class Standard
    {
        // Service info
        public const string ServiceInstanceId = "service.instance.id";
        public const string ServiceName = "service.name";
        public const string ServiceVersion = "service.version";

        // Trace info
        public const string TraceId = "trace.id";
        public const string SpanId = "span.id";

        // HTTP request info (OpenTelemetry semantic conventions)
        public const string HttpMethod = "http.method";
        public const string HttpHost = "http.host";
        public const string HttpTarget = "http.target";
        public const string HttpUserAgent = "http.user_agent";
        public const string HttpContentLength = "http.content_length";
        public const string UrlPath = "url.path";
        public const string UrlQuery = "url.query";

        // Network info
        public const string NetPeerIp = "net.peer.ip";

        // Application correlation
        public const string CorrelationId = "correlation.id";
    }


    public class FwksLabs
    {
        public const string ServicePlatform = "fl.service.platform";
        public const string ServiceNamespace = "fl.service.namespace";
        public const string ServiceMaintainer = "fl.service.maintainer";
    }
}
namespace FwksLabs.Libs.Core.Constants;

public static class HealthCheckProperties
{
    public class Headers
    {
        public const string Client = "X-HealthCheck-Client";
    }

    public class Endpoints
    {
        public const string Liveness = "/health/live";
        public const string Readiness = "/health/ready";
    }

    public class Configuration
    {
        public const int TimeoutInSeconds = 10;
    }

    public class Tags
    {
        public const string Liveness = "liveness";
        public const string Readiness = "readiness";
        public const string Critical = "critical";
        public const string NonCritical = "non-critical";
        public const string TypeDatabase = "type-database";
        public const string TypeHttpService = "type-httpservice";
    }
}
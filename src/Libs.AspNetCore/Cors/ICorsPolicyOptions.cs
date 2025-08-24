namespace FwksLabs.Libs.AspNetCore.Cors;

public sealed record CorsPolicyOptions(
    string Name,
    string[] AllowedHeaders,
    string[] AllowedMethods,
    string[] AllowedOrigins,
    string[] ExposedHeaders,
    bool AllowCredentials,
    int PreflightMaxAgeSeconds);
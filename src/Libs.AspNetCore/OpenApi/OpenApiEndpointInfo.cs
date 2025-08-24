namespace FwksLabs.Libs.AspNetCore.OpenApi;

public record OpenApiEndpointInfo(string? Name, string Summary, string Description, params string[] Tags);
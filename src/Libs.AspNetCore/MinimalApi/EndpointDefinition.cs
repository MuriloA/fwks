namespace FwksLabs.Libs.AspNetCore.MinimalApi;

public record EndpointDefinition
{
    public int Version { get; init; } = 1;
    public string? Name { get; init; } = null;
    public required string Summary { get; init; }
    public required string Description { get; init; }
    public required string[] Tags { get; init; }
}
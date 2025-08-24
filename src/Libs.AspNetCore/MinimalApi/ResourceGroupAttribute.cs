using System;

namespace FwksLabs.Libs.AspNetCore.MinimalApi;

[AttributeUsage(AttributeTargets.Interface)]
public sealed class ResourceGroupAttribute(string prefix) : Attribute
{
    public string Prefix { get; init; } = prefix;
    public string? Tags { get; init; }
    public string? Versions { get; init; }
    public string? Problems { get; init; }
}
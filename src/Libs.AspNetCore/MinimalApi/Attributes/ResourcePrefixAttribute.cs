using System;

namespace FwksLabs.Libs.AspNetCore.MinimalApi.Attributes;

[AttributeUsage(AttributeTargets.Interface)]
public sealed class ResourcePrefixAttribute(string prefix, params string[] tags) : Attribute
{
    public string Prefix { get; init; } = prefix;
    public string[] Tags { get; init; } = tags;
}
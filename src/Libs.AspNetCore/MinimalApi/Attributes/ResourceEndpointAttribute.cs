using System;

namespace FwksLabs.Libs.AspNetCore.MinimalApi.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ResourceEndpointAttribute(Type type) : Attribute
{
    public Type Type { get; } = type;
}
using System;

namespace FwksLabs.Libs.Core.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class EnumMetadataAttribute : Attribute
{
    public string? Label { get; init; }
    public string? Description { get; init; }
}
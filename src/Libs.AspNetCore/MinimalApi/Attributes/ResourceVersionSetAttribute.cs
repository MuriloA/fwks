using System;

namespace FwksLabs.Libs.AspNetCore.MinimalApi.Attributes;

[AttributeUsage(AttributeTargets.Interface)]
public sealed class ResourceVersionSetAttribute(params int[] versions) : Attribute
{
    public int[] Versions { get; set; } = versions;
}
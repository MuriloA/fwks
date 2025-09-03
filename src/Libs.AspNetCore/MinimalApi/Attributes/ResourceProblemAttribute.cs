using System;

namespace FwksLabs.Libs.AspNetCore.MinimalApi.Attributes;

[AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
public sealed class ResourceProblemAttribute(int problem) : Attribute
{
    public int Problem { get; set; } = problem;
}
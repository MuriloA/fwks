using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Asp.Versioning;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.MinimalApi;

public static class MinimalApiConfiguration
{
    public static void MapResourcesFromAssembly<TAssembly>(this IEndpointRouteBuilder builder)
    {
        var types = typeof(TAssembly).Assembly.GetTypes();
        var resourceType = typeof(IResource);

        foreach (var resource in types.Where(IsResourceGroup))
        {
            var attr = resource.GetCustomAttribute<ResourceGroupAttribute>();

            if (attr is null)
                continue;

            builder
                .MapGroup($"v{{version:apiVersion}}/{attr.Prefix}")
                .ConfigureTags(attr.Tags, attr.Prefix)
                .ConfigureProblems(attr.Problems)
                .ConfigureVersionSet(attr.Versions)
                .ConfigureEndpoints(types, resource);
        }

        return;

        bool IsResourceGroup(Type type)
        {
            return type.IsInterface && type.IsAssignableTo(resourceType) && type != resourceType;
        }
    }

    private static RouteGroupBuilder ConfigureTags(this RouteGroupBuilder builder, string? tags, string fallback)
    {
        return builder
            .WithTags(tags.IsNullOrWhiteSpace()
                ? [fallback.Pascalize()]
                : tags.ToArrayOf<string>());
    }

    private static RouteGroupBuilder ConfigureProblems(this RouteGroupBuilder builder, string? problems)
    {
        var uniqueProblems = new List<int> { StatusCodes.Status500InternalServerError }
            .Concat(problems.IsNullOrWhiteSpace() ? [] : problems.ToArrayOf<int>())
            .Distinct();

        foreach (var problem in uniqueProblems)
            builder.ProducesProblem(problem);

        return builder;
    }

    private static RouteGroupBuilder ConfigureVersionSet(this RouteGroupBuilder builder, string? versions)
    {
        var availableVersions = versions.IsNullOrWhiteSpace() ? [1] : versions.ToArrayOf<int>();

        var set = builder.NewApiVersionSet().ReportApiVersions();

        foreach (var version in availableVersions)
            set.HasApiVersion(new ApiVersion(version));

        builder.WithApiVersionSet(set.Build());

        return builder;
    }

    private static void ConfigureEndpoints(this RouteGroupBuilder builder, Type[] types, Type resourceType)
    {
        var endpoints = types
            .Where(x => x.IsClass && resourceType.IsAssignableFrom(x))
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>()
            .ToArray();

        foreach (var endpoint in endpoints)
            endpoint.Map(builder);
    }
}
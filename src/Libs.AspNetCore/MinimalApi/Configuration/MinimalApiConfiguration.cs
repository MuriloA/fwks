using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Asp.Versioning;
using FwksLabs.Libs.AspNetCore.MinimalApi.Abstractions;
using FwksLabs.Libs.AspNetCore.MinimalApi.Attributes;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.MinimalApi.Configuration;

public static class MinimalApiConfiguration
{
    public static void MapResourcesFromAssembly<TAssembly>(this IEndpointRouteBuilder builder)
    {
        var types = typeof(TAssembly).Assembly.GetTypes();
        var resourceType = typeof(IResource);

        foreach (var resource in types.Where(IsResourceGroup))
        {
            var attr = resource.GetCustomAttribute<ResourcePrefixAttribute>();

            if (attr is null)
                continue;

            builder
                .MapGroup($"v{{version:apiVersion}}/{attr.Prefix}")
                .ConfigureTags(attr, attr.Prefix)
                .ConfigureProblems(resource)
                .ConfigureVersionSet(resource)
                .ConfigureEndpoints(types, resource);
        }

        return;

        bool IsResourceGroup(Type type)
        {
            return type.IsInterface && type.IsAssignableTo(resourceType) && type != resourceType;
        }
    }

    private static RouteGroupBuilder ConfigureTags(this RouteGroupBuilder builder, ResourcePrefixAttribute attr, string fallback)
    {
        var tags = attr.Tags.Select(x => x.IsNullOrWhiteSpace() is false);

        return builder
            .WithTags(tags.Any()
                ? attr.Tags
                : [fallback.Pascalize()]);
    }

    private static RouteGroupBuilder ConfigureProblems(this RouteGroupBuilder builder, Type resource)
    {
        var uniqueProblems = new List<int> { StatusCodes.Status500InternalServerError }
            .Concat(resource.GetCustomAttributes<ResourceProblemAttribute>()?.Select(x => x.Problem) ?? [])
            .Distinct();

        foreach (var problem in uniqueProblems)
            builder.ProducesProblem(problem);

        return builder;
    }

    private static RouteGroupBuilder ConfigureVersionSet(this RouteGroupBuilder builder, Type resource)
    {
        var availableVersions = resource.GetCustomAttribute<ResourceVersionSetAttribute>()?.Versions ?? [1];

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
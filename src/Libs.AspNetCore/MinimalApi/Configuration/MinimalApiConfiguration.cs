using System;
using System.Linq;
using System.Reflection;
using Asp.Versioning;
using FwksLabs.Libs.AspNetCore.MinimalApi.Abstractions;
using FwksLabs.Libs.AspNetCore.MinimalApi.Attributes;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.MinimalApi.Configuration;

public static class MinimalApiConfiguration
{
    public static void MapResourcesFromAssembly<TAssembly>(this IEndpointRouteBuilder builder)
    {
        var types = typeof(TAssembly).Assembly.GetTypes();

        foreach (var resourceType in types.Where(IsResourceDefinition))
        {
            var resource = (ResourceDefinition)Activator.CreateInstance(resourceType)!;

            var group = builder
                .MapGroup($"v{{version:apiVersion}}/{resource.Prefix}")
                .ConfigureTags(resource)
                .ConfigureProblems(resource)
                .ConfigureVersionSet(resource);

            resource.Configure(group);

            group.ConfigureEndpoints(types, resourceType, resource);
        }

        return;

        bool IsResourceDefinition(Type t)
        {
            return t.IsAbstract is not true && t.IsSubclassOf(typeof(ResourceDefinition));
        }
    }

    private static RouteGroupBuilder ConfigureTags(this RouteGroupBuilder builder, ResourceDefinition definition)
    {
        return builder.WithTags([.. definition.Tags.Where(x => x.IsNullOrWhiteSpace() is not true)]);
    }

    private static RouteGroupBuilder ConfigureProblems(this RouteGroupBuilder builder, ResourceDefinition definition)
    {
        foreach (var problem in definition.Problems.Concat([StatusCodes.Status500InternalServerError]).Distinct())
            builder.ProducesProblem(problem);

        return builder;
    }

    private static RouteGroupBuilder ConfigureVersionSet(this RouteGroupBuilder builder, ResourceDefinition definition)
    {
        var set = builder.NewApiVersionSet().ReportApiVersions();

        foreach (var version in definition.Versions)
            set.HasApiVersion(new ApiVersion(version));

        builder.WithApiVersionSet(set.Build());

        return builder;
    }

    private static void ConfigureEndpoints(this RouteGroupBuilder builder, Type[] types, Type resourceType, ResourceDefinition definition)
    {
        var endpoints = types
            .Where(x => x is { IsClass: true, IsAbstract: false })
            .Where(x => x.GetCustomAttribute<ResourceEndpointAttribute>()?.Type == resourceType)
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>()
            .ToArray();

        foreach (var endpoint in endpoints)
            endpoint
                .Map(builder)
                .WithTags(definition.Tags);
    }
}
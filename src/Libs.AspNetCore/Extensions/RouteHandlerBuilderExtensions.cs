using FwksLabs.Libs.AspNetCore.Filters;
using FwksLabs.Libs.AspNetCore.OpenApi;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Extensions;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder AddRequestValidatorFilter<TRequest>(this RouteHandlerBuilder builder)
        where TRequest : notnull
    {
        return builder.AddEndpointFilter<FluentValidationFilter<TRequest>>();
    }

    public static RouteHandlerBuilder WithEndpointInfo(this RouteHandlerBuilder builder,
        string summary, string description, params string[] tags)
    {
        return builder
            .WithName(summary.Pascalize())
            .WithSummary(summary)
            .WithDescription(description)
            .WithTags(tags);
    }

    public static RouteHandlerBuilder WithEndpointInfo(this RouteHandlerBuilder builder, OpenApiEndpointInfo meta)
    {
        return builder
            .WithName(meta.Name ?? meta.Summary.Pascalize())
            .WithSummary(meta.Summary)
            .WithDescription(meta.Description)
            .WithTags(meta.Tags);
    }

    public static RouteHandlerBuilder ProducesProblems(this RouteHandlerBuilder builder, params int[] statusCodes)
    {
        foreach (var statusCode in statusCodes)
            builder.ProducesProblem(statusCode);

        return builder;
    }
}
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

    public static RouteHandlerBuilder WithEndpointInfo(this RouteHandlerBuilder builder, OpenApiEndpointInfo info)
    {
        return builder
            .WithName(info.Name ?? info.Summary.Pascalize())
            .WithSummary(info.Summary)
            .WithDescription(info.Description)
            .WithTags(info.Tags);
    }

    public static RouteHandlerBuilder ProducesProblems(this RouteHandlerBuilder builder, params int[] statusCodes)
    {
        foreach (var statusCode in statusCodes)
            builder.ProducesProblem(statusCode);

        return builder;
    }
}
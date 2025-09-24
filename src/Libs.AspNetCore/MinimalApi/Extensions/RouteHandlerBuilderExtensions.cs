using FwksLabs.Libs.AspNetCore.Filters;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.MinimalApi.Extensions;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
        where TRequest : notnull =>
        builder.AddEndpointFilter<FluentValidationFilter<TRequest>>();

    public static RouteHandlerBuilder WithDefinition(this RouteHandlerBuilder builder, EndpointDefinition definition) =>
        builder
            .WithName(definition.Name ?? definition.Summary.Pascalize())
            .WithSummary(definition.Summary)
            .WithDescription(definition.Description)
            .WithTags(definition.Tags)
            .MapToApiVersion(definition.Version);

    public static RouteHandlerBuilder ProducesProblems(this RouteHandlerBuilder builder, params int[] statusCodes)
    {
        foreach (var statusCode in statusCodes)
            builder.ProducesProblem(statusCode);

        return builder;
    }
}
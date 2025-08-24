using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentValidation.Results;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FwksLabs.Libs.AspNetCore.Constants;

public static class AppResponses
{
    // 2XX - Success
    public static IResult Ok()
    {
        return TypedResults.Ok();
    }

    public static IResult Ok<T>(T value)
    {
        return TypedResults.Ok(value);
    }

    public static IResult Created()
    {
        return TypedResults.Created();
    }

    public static IResult Created<T>(T value)
    {
        return TypedResults.Created(string.Empty, value);
    }

    public static IResult Created<T>(string uri, T value)
    {
        return TypedResults.Created(uri, value);
    }

    public static IResult Accepted()
    {
        return TypedResults.Accepted(string.Empty);
    }

    public static IResult Accepted<T>(T value)
    {
        return TypedResults.Accepted(string.Empty, value);
    }

    public static IResult Accepted<T>(string uri, T value)
    {
        return TypedResults.Accepted(uri, value);
    }

    public static IResult NoContent()
    {
        return TypedResults.NoContent();
    }

    // 4XX - Client Errors
    public static IResult ValidationErrors(ValidationResult validationResult)
    {
        var errors = validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key.Camelize(),
                g => g.Select(e => e.ErrorMessage).ToArray());

        return Problem(
            HttpStatusCode.BadRequest,
            "Validation Errors",
            "Some fields failed validation. Check the input data.",
            new Dictionary<string, object?> { { "errors", errors } });
    }

    public static IResult BadRequest(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.BadRequest, title, detail, extensions, instance);
    }

    public static IResult Unauthorized(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.Unauthorized, title, detail, extensions, instance);
    }

    public static IResult Forbidden(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.Forbidden, title, detail, extensions, instance);
    }

    public static IResult NotFound(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.NotFound, title, detail, extensions, instance);
    }

    public static IResult NotAcceptable(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.NotAcceptable, title, detail, extensions, instance);
    }

    public static IResult Conflict(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.Conflict, title, detail, extensions, instance);
    }

    public static IResult TooManyRequests(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.TooManyRequests, title, detail, extensions, instance);
    }

    public static IResult UnprocessableEntity(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.UnprocessableEntity, title, detail, extensions, instance);
    }

    // 5XX - Server Errors
    public static IResult InternalServerError(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.InternalServerError, title, detail, extensions, instance);
    }

    public static IResult BadGateway(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.BadGateway, title, detail, extensions, instance);
    }

    public static IResult ServiceUnavailable(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.ServiceUnavailable, title, detail, extensions, instance);
    }

    public static IResult GatewayTimeout(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        return Problem(HttpStatusCode.GatewayTimeout, title, detail, extensions, instance);
    }

    public static ProblemHttpResult Problem(HttpStatusCode statusCode, string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null)
    {
        var type = $"https://docs.fwkslabs.com/errors/http/{statusCode.ToString().Kebaberize().ToLower()}";
        title ??= statusCode.ToString().Humanize(LetterCasing.Title);

        return TypedResults.Problem(
            detail,
            instance,
            (int)statusCode,
            title,
            type,
            extensions);
    }
}
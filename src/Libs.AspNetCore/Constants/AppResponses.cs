using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FwksLabs.Libs.AspNetCore.Constants;

public static class AppResponses
{
    // 2XX - Success
    public static IResult Ok() => TypedResults.Ok();

    public static IResult Ok<T>(T value) => TypedResults.Ok(value);

    public static IResult Created() => TypedResults.Created();

    public static IResult Created<T>(T value) => TypedResults.Created(string.Empty, value);

    public static IResult Created<T>(string uri, T value) => TypedResults.Created(uri, value);

    public static IResult Accepted() => TypedResults.Accepted(string.Empty);

    public static IResult Accepted<T>(T value) => TypedResults.Accepted(string.Empty, value);

    public static IResult Accepted<T>(string uri, T value) => TypedResults.Accepted(uri, value);

    public static IResult NoContent() => TypedResults.NoContent();

    // 4XX - Client Errors
    public static IResult ValidationErrors(ValidationResult validationResult) =>
        Problem(
            HttpStatusCode.BadRequest,
            "Validation Errors",
            "Some fields failed validation. Check the input data.",
            validationResult.ToErrorDictionary());

    public static IResult BadRequest(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.BadRequest, title, detail, extensions, instance);

    public static IResult BadRequest(string? title, string detail, ValidationResult validationResult, string? instance = null) =>
        Problem(HttpStatusCode.BadRequest, title, detail, validationResult.ToErrorDictionary(), instance);

    public static IResult Unauthorized(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.Unauthorized, title, detail, extensions, instance);

    public static IResult Forbidden(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.Forbidden, title, detail, extensions, instance);

    public static IResult NotFound(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.NotFound, title, detail, extensions, instance);

    public static IResult NotAcceptable(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.NotAcceptable, title, detail, extensions, instance);

    public static IResult Conflict(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.Conflict, title, detail, extensions, instance);

    public static IResult TooManyRequests(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.TooManyRequests, title, detail, extensions, instance);

    public static IResult UnprocessableEntity(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.UnprocessableEntity, title, detail, extensions, instance);

    // 5XX - Server Errors
    public static IResult InternalServerError(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.InternalServerError, title, detail, extensions, instance);

    public static IResult BadGateway(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.BadGateway, title, detail, extensions, instance);

    public static IResult ServiceUnavailable(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.ServiceUnavailable, title, detail, extensions, instance);

    public static IResult GatewayTimeout(string? title, string detail, IDictionary<string, object?>? extensions = null, string? instance = null) =>
        Problem(HttpStatusCode.GatewayTimeout, title, detail, extensions, instance);

    public static ProblemHttpResult Problem(HttpStatusCode statusCode, string? title, string detail, IDictionary<string, object?>? errors = null, string? instance = null)
    {
        var type = $"https://docs.fwkslabs.com/errors/http/{statusCode.ToString().Kebaberize().ToLower()}";
        title ??= statusCode.ToString().Humanize(LetterCasing.Title);

        return TypedResults.Problem(
            detail,
            instance,
            (int)statusCode,
            title,
            type,
            errors);
    }
}
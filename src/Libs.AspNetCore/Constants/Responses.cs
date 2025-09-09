using System.Collections.Generic;
using FwksLabs.Libs.AspNetCore.Results;
using FwksLabs.Libs.Core.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FwksLabs.Libs.AspNetCore.Constants;

public static class Responses
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

    public static IResult Defer() => new DeferredResult();

    // Problems
    public static ProblemHttpResult Problem(
        ApplicationError error,
        IDictionary<int, int> errorCodes,
        IDictionary<string, object?>? extensions) =>
        Problem(error, errorCodes, null, extensions);

    public static ProblemHttpResult Problem(
        ApplicationError error,
        IDictionary<int, int> errorCodes,
        string? detail = null,
        IDictionary<string, object?>? extensions = null,
        string? instance = null) =>
        TypedResults.Problem(
            detail ?? error.Detail, instance, errorCodes[error.Code], error.Title, $"https://docs.fwkslabs.com/errors/{error.Code}", extensions);
}
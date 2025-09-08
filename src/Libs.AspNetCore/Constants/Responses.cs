using System.Collections.Generic;
using FwksLabs.Libs.AspNetCore.Results;
using FwksLabs.Libs.Core.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FwksLabs.Libs.AspNetCore.Constants;

public static class Responses
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

    public static IResult Defer()
    {
        return new DeferredResult();
    }

    // Problems
    public static ProblemHttpResult Problem(
        ApplicationError error,
        IDictionary<int, int> errorCodes,
        IDictionary<string, object?>? extensions)
    {
        return Problem(error, errorCodes, null, extensions);
    }

    public static ProblemHttpResult Problem(
        ApplicationError error,
        IDictionary<int, int> errorCodes,
        string? detail = null,
        IDictionary<string, object?>? extensions = null,
        string? instance = null)
    {
        return TypedResults.Problem(
            detail ?? error.Detail, instance, errorCodes[error.Code], error.Title, $"https://docs.fwkslabs.com/errors/{error.Code}", extensions);
    }
}
using System.Collections.Generic;
using FluentValidation.Results;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Constants;

public static class AppResults
{
    public static Result<TData> Empty<TData>() where TData : notnull
    {
        return new Result<TData>();
    }

    public static Result<TData> Success<TData>(TData data) where TData : notnull
    {
        return new Result<TData> { Data = data };
    }

    public static Result<TData> Failure<TData>(IDictionary<string, object?> messages) where TData : notnull
    {
        return new Result<TData> { Messages = messages };
    }

    public static Result<TData> Failure<TData>(string messageTitle, object? messageDetail) where TData : notnull
    {
        return Empty<TData>().AddMessage(messageTitle, messageDetail);
    }

    public static Result<TData> Failure<TData>(ValidationResult validationResult) where TData : notnull
    {
        return new Result<TData> { Messages = validationResult.GetMessages() };
    }
}
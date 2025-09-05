using System.Collections.Generic;
using FluentValidation.Results;
using FwksLabs.Libs.Core.Extensions;

namespace FwksLabs.Libs.Core.Types;

public record Result
{
    protected Result()
    {
    }

    public bool IsSuccess => Errors.Count == 0;

    public IDictionary<string, object?> Errors { get; init; } = new Dictionary<string, object?>();

    public static implicit operator Result(ValidationResult validationResult) => Failure(validationResult);

    public static Result Success() => new();

    public static Result Failure(IDictionary<string, object?> messages) => new() { Errors = messages };

    public static Result Failure(string title, object? detail = null) => new() { Errors = new Dictionary<string, object?> { { title, detail } } };

    public static Result Failure(ValidationResult validationResult) => new() { Errors = validationResult.ToErrorDictionary() };

    public static Result<T> Success<T>(T value) => Result<T>.Create(value, new Dictionary<string, object?>());

    public static Result<T> Failure<T>(IDictionary<string, object?> messages) => Result<T>.Create(default, messages);

    public static Result<T> Failure<T>(string title, object? detail = null) => Result<T>.Create(default, new Dictionary<string, object?> { { title, detail } });

    public static Result<T> Failure<T>(ValidationResult validationResult) => Result<T>.Create(default, validationResult.ToErrorDictionary());
}

public record Result<T> : Result
{
    protected Result()
    {
    }

    public T? Value { get; init; }

    public static implicit operator Result<T>(T? value) => value is null ? Failure<T>(new Dictionary<string, object?>()) : Success(value);

    public static implicit operator Result<T>(ValidationResult validationResult) => Failure<T>(validationResult);

    public static Result<T> Create(T? value, IDictionary<string, object?> messages) => new() { Value = value, Errors = messages };
}
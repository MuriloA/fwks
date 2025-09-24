using System;
using FluentValidation;

namespace FwksLabs.Libs.Core.Extensions;

public static partial class FluentValidationExtensions
{
    // DateTimeOffset
    public static IRuleBuilderOptions<T, DateTimeOffset> BeInThePast<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder) =>
        ruleBuilder
            .Must(date => date < DateTimeOffset.UtcNow).WithMessage("'{PropertyName}' must be a date in the past.");

    public static IRuleBuilderOptions<T, DateTimeOffset> BeInTheFuture<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder) =>
        ruleBuilder
            .Must(date => date > DateTimeOffset.UtcNow).WithMessage("'{PropertyName}' must be a date in the future.");

    public static IRuleBuilderOptions<T, DateTimeOffset> NotBeInThePast<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder) =>
        ruleBuilder
            .Must(date => date >= DateTimeOffset.UtcNow).WithMessage("'{PropertyName}' must not be a date in the past.");

    public static IRuleBuilderOptions<T, DateTimeOffset> NotBeInTheFuture<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder) =>
        ruleBuilder
            .Must(date => date <= DateTimeOffset.UtcNow).WithMessage("'{PropertyName}' must not be a date in the future.");

    public static IRuleBuilderOptions<T, DateTimeOffset> BeforeDate<T>(
        this IRuleBuilder<T, DateTimeOffset> ruleBuilder, Func<T, DateTimeOffset> dateSelector) =>
        ruleBuilder
            .Must((entity, date) => date < dateSelector(entity))
            .WithMessage((entity, _) => $"'{{PropertyName}}' must be before {dateSelector(entity):yyyy-MM-dd HH:mm:ss:fff}.");

    public static IRuleBuilderOptions<T, DateTimeOffset> AfterDate<T>(
        this IRuleBuilder<T, DateTimeOffset> ruleBuilder, Func<T, DateTimeOffset> dateSelector) =>
        ruleBuilder
            .Must((entity, date) => date > dateSelector(entity))
            .WithMessage((entity, _) => $"'{{PropertyName}}' must be after {dateSelector(entity):yyyy-MM-dd HH:mm:ss:fff}.");

    // DateOnly
    public static IRuleBuilderOptions<T, DateOnly> BeInThePast<T>(this IRuleBuilder<T, DateOnly> ruleBuilder) =>
        ruleBuilder
            .Must(date => date < DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("'{PropertyName}' must be a date in the past.");

    public static IRuleBuilderOptions<T, DateOnly> BeInTheFuture<T>(this IRuleBuilder<T, DateOnly> ruleBuilder) =>
        ruleBuilder
            .Must(date => date > DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("'{PropertyName}' must be a date in the future.");

    public static IRuleBuilderOptions<T, DateOnly> NotBeInThePast<T>(this IRuleBuilder<T, DateOnly> ruleBuilder) =>
        ruleBuilder
            .Must(date => date >= DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("'{PropertyName}' must not be a date in the past.");

    public static IRuleBuilderOptions<T, DateOnly> NotBeInTheFuture<T>(this IRuleBuilder<T, DateOnly> ruleBuilder) =>
        ruleBuilder
            .Must(date => date <= DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("'{PropertyName}' must not be a date in the future.");

    public static IRuleBuilderOptions<T, DateOnly> BeforeDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder, Func<T, DateOnly> dateSelector) =>
        ruleBuilder
            .Must((entity, date) => date < dateSelector(entity))
            .WithMessage((entity, _) => $"'{{PropertyName}}' must be before {dateSelector(entity):yyyy-MM-dd}.");

    public static IRuleBuilderOptions<T, DateOnly> AfterDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder, Func<T, DateOnly> dateSelector) =>
        ruleBuilder
            .Must((entity, date) => date > dateSelector(entity))
            .WithMessage((entity, _) => $"'{{PropertyName}}' must be after {dateSelector(entity):yyyy-MM-dd}.");
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Types;
using Humanizer;

namespace FwksLabs.Libs.Core.Extensions;

public static class FluentValidationExtensions
{
    public static IDictionary<string, object?> ToErrorDictionary(this ValidationResult result)
    {
        var errors = result.Errors.Select(failure =>
        {
            var isCollection = failure.FormattedMessagePlaceholderValues.TryGetValue("CollectionIndex", out var index);

            if (isCollection is false)
                return failure;

            return new NormalizedValidationFailure(
                failure.FormattedMessagePlaceholderValues["PropertyName"].ToString()!,
                failure.ErrorMessage,
                int.Parse(index!.ToString()!));
        });

        return new Dictionary<string, object?>
        {
            {
                "errors", errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        errGroup => errGroup.Key.Camelize(),
                        errGroup => errGroup.GroupBy(x => x.Index)
                            .ToDictionary(
                                ixGroup => ixGroup.Key,
                                ixGroup => ixGroup.Select(x => x.ErrorMessage)))
            }
        };
    }

    public static IRuleBuilderOptions<T, string?> PhoneNumber<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(value => value is not null && ApplicationRegex.PhoneNumber().IsMatch(value.ClearSpaces()))
            .WithMessage("{PropertyName} must be a valid phone number starting with + country code plus number.");
    }

    public static IRuleBuilderOptions<T, DateTimeOffset> NotInThePast<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date < DateTimeOffset.UtcNow)
            .WithMessage("{PropertyName} must a date in the present, not in the past.");
    }

    public static IRuleBuilderOptions<T, DateTimeOffset> NotInTheFuture<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date > DateTimeOffset.UtcNow)
            .WithMessage("{PropertyName} must a date in the past, not in the future.");
    }

    public static IRuleBuilderOptions<T, DateTimeOffset> BeforeDate<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, Func<T, DateTimeOffset> dateSelector)
    {
        return ruleBuilder
            .Must((entity, date) => date < dateSelector(entity))
            .WithMessage((entity, _) => $"{{PropertyName}} must be before {dateSelector(entity):yyyy-MM-dd}.");
    }

    public static IRuleBuilderOptions<T, DateTimeOffset> AfterDate<T>(this IRuleBuilder<T, DateTimeOffset> ruleBuilder, Func<T, DateTimeOffset> dateSelector)
    {
        return ruleBuilder
            .Must((entity, date) => date < dateSelector(entity))
            .WithMessage((entity, _) => $"{{PropertyName}} must be after {dateSelector(entity):yyyy-MM-dd}.");
    }

    public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(url =>
                Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            .WithMessage("'{PropertyName}' must be a valid URL (http or https).");
    }
}
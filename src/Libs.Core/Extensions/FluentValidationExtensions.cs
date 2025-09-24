using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FwksLabs.Libs.Core.Constants;
using Humanizer;

namespace FwksLabs.Libs.Core.Extensions;

public static partial class FluentValidationExtensions
{
    public static IDictionary<string, object?> NormalizeErrors(this ValidationResult result)
    {
        return new Dictionary<string, object?>
        {
            {
                "errors", result.Errors.Select(x => new
                {
                    Path = Normalize(x.PropertyName),
                    Message = x.ErrorMessage
                })
            }
        };

        string Normalize(string name)
        {
            var segments = name.Replace('[', '/').Replace("]", string.Empty).Replace('.', '/').Split('/');

            return $"/{string.Join('/', segments.Select(s => s.Camelize()))}";
        }
    }

    public static IRuleBuilderOptions<T, string?> PhoneNumber<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(value => value is not null && ApplicationRegex.PhoneNumber().IsMatch(value.ClearSpaces()))
            .WithMessage("'{PropertyName}' must be a valid phone number starting with + country code plus number.");
    }

    public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(url =>
                Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            .WithMessage("'{PropertyName}' must be a valid URL (http or https).");
    }

    public static IRuleBuilderOptions<T, string?> HexColor<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(value => value.IsNullOrWhiteSpace() is not true && ApplicationRegex.HexColor().IsMatch(value))
            .WithMessage("'{PropertyName}' must be a valid HEX color. e.g #000, 000, #000FFF, 000FFF");
    }

    public static IRuleBuilderOptions<T, string?> StartsWithPattern<T>(this IRuleBuilder<T, string?> ruleBuilder, string pattern)
    {
        return ruleBuilder
            .Must(value => value.IsNullOrWhiteSpace() is not true && value.StartsWith(pattern))
            .WithMessage($"'{{PropertyName}}' must start with the pattern '{pattern}'.");
    }
}
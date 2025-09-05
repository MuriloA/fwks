using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FwksLabs.Libs.Core.Encoders;
using Humanizer;

namespace FwksLabs.Libs.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? input) => string.IsNullOrWhiteSpace(input);

    public static TOutput[] ToArrayOf<TOutput>(this string input, char separator = ',')
    {
        var type = typeof(TOutput);

        return input
            .Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => Convert.ChangeType(s, type))
            .Cast<TOutput>()
            .ToArray();
    }

    public static bool EqualsTo(this string? input, string target) => string.Equals(input, target, StringComparison.InvariantCultureIgnoreCase);

    public static bool HasUniqueCharacters(this string input) => input.All(new HashSet<char>().Add);

    public static Guid Decode(this string input) => Base62Encoder.Decode(input);

    public static string PluralizeEntity(this string name) =>
        name.Contains("Entity")
            ? name[..^"Entity".Length].Pluralize()
            : name.Pluralize();

    public static string Format(this string input, params object[] args) => string.Format(input, args);

    public static bool IsValidTimeZone(this string? id)
    {
        try
        {
            if (id.IsNullOrWhiteSpace())
                return false;

            _ = TimeZoneInfo.FindSystemTimeZoneById(id);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string ClearSpaces(this string input) => input.Replace(" ", string.Empty);

    public static DateTime ToDateTime(this string input)
    {
        if (DateTime.TryParse(input, out var datetime) is false)
            throw new FormatException("Input string was not recognized as a DateTime");

        return datetime;
    }

    public static DateTimeOffset ToDateTimeOffset(this string input)
    {
        if (DateTimeOffset.TryParse(input, out var datetime) is false)
            throw new FormatException("Input string was not recognized as a DateTime");

        return datetime;
    }
}
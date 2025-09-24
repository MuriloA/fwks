using System.Text.RegularExpressions;

namespace FwksLabs.Libs.Core.Constants;

public abstract partial class ApplicationRegex
{
    [GeneratedRegex(@"^\+\d{1,3}\d{5,14}$", RegexOptions.Compiled)]
    public static partial Regex PhoneNumber();

    [GeneratedRegex("[^a-zA-Z0-9]", RegexOptions.Compiled)]
    public static partial Regex Alphanumeric();

    [GeneratedRegex("[^a-zA-Z0-9_ -]", RegexOptions.Compiled)]
    public static partial Regex Slug();

    [GeneratedRegex("^GMT[+-](?:0?[0-9]|1[0-4])$", RegexOptions.Compiled)]
    public static partial Regex GMT();

    [GeneratedRegex("^#?([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$", RegexOptions.Compiled)]
    public static partial Regex HexColor();
}
using System.Text.RegularExpressions;

namespace FwksLabs.Libs.Core.Constants;

public abstract partial class AppRegex
{
    [GeneratedRegex(@"^\+\d{1,3}\d{5,14}$")]
    public static partial Regex PhoneNumber();

    [GeneratedRegex("[^a-zA-Z0-9]")]
    public static partial Regex Alphanumeric();

    [GeneratedRegex("[^a-zA-Z0-9_ -]")]
    public static partial Regex Slug();

    [GeneratedRegex(@"^GMT[+-](?:0?[0-9]|1[0-4])$")]
    public static partial Regex GMT();
}
namespace FwksLabs.Libs.AspNetCore.Filters.SupportKey;

public sealed record SupportKeyFilterOptions
{
    public required string Token { get; set; }
}
namespace FwksLabs.Libs.Core.Types;

/// <summary>
///     Standardized, generic application error definitions.
///     Categorized by numeric ranges for clarity and extensibility.
///     Categories:
///     0x000 – 0x0FF : General / Infrastructure
///     0x100 – 0x1FF : Validation / Input
///     0x200 – 0x2FF : Security / Permissions
///     0x300 – 0x3FF : Concurrency / State
///     0x400 – 0x4FF : External / Integration
///     0xF000 – 0xFFFF : Custom / Reserved for Services
/// </summary>
public sealed record ApplicationError(int Code, string Name, string Title, string Detail, string? Instance = null)
{
    public static implicit operator int(ApplicationError error) => error.Code;
}
namespace FwksLabs.Libs.Core.Types;

/// <summary>
/// Configures the translation of enum metadata
/// </summary>
/// <param name="LabelFallback">Defines rather the raw value will be used as fallback when no label is provided.</param>
/// <param name="DescriptionFallback">Defines rather the raw value will be used as fallback when no description is provided.</param>
public sealed record EnumMetadataOptions(bool LabelFallback, bool DescriptionFallback);
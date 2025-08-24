using System;
using System.Linq;
using FwksLabs.Libs.Core.Attributes;

namespace FwksLabs.Libs.Core.Types;

public sealed record EnumMetadata(int Id, string Label, string Description)
{
    public static EnumMetadata From<TEnum>(TEnum value, Action<EnumMetadataOptions>? optionsAction = null) where TEnum : Enum
    {
        EnumMetadataOptions options = new(false, false);

        optionsAction?.Invoke(options);

        var fieldInfo = value.GetType().GetField(value.ToString());
        var attribute = fieldInfo?.GetCustomAttributes(typeof(EnumMetadataAttribute), false).FirstOrDefault() as EnumMetadataAttribute;

        return new EnumMetadata(
            Convert.ToInt32(value),
            attribute?.Label is null && options.LabelFallback ? value.ToString() : string.Empty,
            attribute?.Description is null && options.DescriptionFallback ? value.ToString() : string.Empty);
    }
}
using System;

namespace FwksLabs.Libs.Core.Types;

public static class GuidV7
{
    private static DateTimeOffset? _offset;

    public static void SetOffset(DateTimeOffset offset)
    {
        _offset = offset;
    }

    public static void ResetOffset()
    {
        _offset = null;
    }

    public static Guid Create()
    {
        return _offset is null
            ? Guid.CreateVersion7()
            : Guid.CreateVersion7(_offset.Value);
    }
}
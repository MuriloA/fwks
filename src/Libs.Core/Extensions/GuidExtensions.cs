using System;
using FwksLabs.Libs.Core.Encoders;

namespace FwksLabs.Libs.Core.Extensions;

public static class GuidExtensions
{
    public static string Encode(this Guid guid) => Base62Encoder.Encode(guid);
}
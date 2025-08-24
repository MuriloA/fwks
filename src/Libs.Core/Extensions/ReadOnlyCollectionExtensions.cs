using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Extensions;

public static class ReadOnlyCollectionExtensions
{
    public static bool CountBiggerThan<T>(this IReadOnlyCollection<T> collection, int count)
    {
        return collection.Count > count;
    }

    public static bool CountBiggerThanZero<T>(this IReadOnlyCollection<T> collection)
    {
        return collection.CountBiggerThan(0);
    }
}
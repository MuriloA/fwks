using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Extensions;

public static class ReadOnlyCollectionExtensions
{
    public static bool CountBiggerThan<T>(this IReadOnlyCollection<T> collection, int count) => collection.Count > count;

    public static bool CountBiggerThanZero<T>(this IReadOnlyCollection<T> collection) => collection.CountBiggerThan(0);
}
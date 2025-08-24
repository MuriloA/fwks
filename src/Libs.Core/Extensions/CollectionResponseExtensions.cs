using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Extensions;

public static class CollectionResponseExtensions
{
    public static CollectionResponse<TData> ToCollectionResponse<TSource, TData>(this IReadOnlyCollection<TSource> collection, Func<TSource, TData> transform)
    {
        if (collection.Count == 0)
            return new CollectionResponse<TData>(0, []);

        return new CollectionResponse<TData>(
            collection.Count,
            [.. collection.Select(transform)]
        );
    }
}
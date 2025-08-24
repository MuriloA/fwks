using System;
using System.Linq;
using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Extensions;

public static class PageResponseExtensions
{
    public static PageResponse<TData> ToPageResponse<TSource, TData>(this Page<TSource>? page, Func<TSource, TData> transform)
    {
        if (page is null || page.Items.Count == 0)
            return new PageResponse<TData>(1, -1, 0, []);

        return new PageResponse<TData>(
            page.PageNumber,
            page.PageSize,
            page.TotalItems,
            [.. page.Items.Select(transform)]
        );
    }
}
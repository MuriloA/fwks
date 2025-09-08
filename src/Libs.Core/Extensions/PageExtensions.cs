using System;
using System.Linq;
using FwksLabs.Libs.Core.Types;

namespace FwksLabs.Libs.Core.Extensions;

public static class PageExtensions
{
    public static Page<TData> TransformPage<TEntity, TData>(this Page<TEntity> page, Func<TEntity, TData> transform)
    {
        return new Page<TData>(
            page.PageNumber,
            page.PageSize,
            page.TotalItems,
            [.. page.Items.Select(transform)]
        );
    }
}
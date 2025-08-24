using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Types;

public record Page<TEntity>(int PageNumber, int PageSize, int TotalItems, IReadOnlyCollection<TEntity> Items)
{
    public static Page<TEntity> Empty()
    {
        return new Page<TEntity>(1, -1, 0, []);
    }
}
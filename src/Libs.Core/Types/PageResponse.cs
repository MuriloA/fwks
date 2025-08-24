using System;
using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Types;

public record PageResponse<T>
{
    public PageResponse(int pageNumber, int pageSize, int totalItems, IReadOnlyCollection<T> items)
    {
        PageNumber = pageNumber;
        PageSize = pageSize < 1 ? -1 : pageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling((double)totalItems / (pageSize < 1 ? totalItems : pageSize));
        Items = items;

        HasPrevious = PageNumber > 1;
        HasNext = PageNumber < TotalPages;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public bool HasPrevious { get; }
    public bool HasNext { get; }
    public IReadOnlyCollection<T> Items { get; }

    public static PageResponse<T> Create(int pageNumber, int pageSize, int totalItems, IReadOnlyCollection<T> items)
    {
        return new PageResponse<T>(pageNumber, pageSize, totalItems, items);
    }
}
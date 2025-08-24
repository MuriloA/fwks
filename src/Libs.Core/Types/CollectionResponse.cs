using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Types;

public record CollectionResponse<TData>(int TotalItems, IEnumerable<TData> Items);
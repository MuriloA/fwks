using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Types;

public record Result<TData> where TData : notnull
{
    internal Result()
    {
    }

    public TData? Data { get; init; }
    public IDictionary<string, object?> Messages { get; init; } = new Dictionary<string, object?>();
    public bool IsSuccess
    {
        get { return Messages.Count == 0; }
    }

    public Result<TData> AddMessage(string title, object? detail)
    {
        _ = Messages.TryAdd(title, detail);

        return this;
    }
}
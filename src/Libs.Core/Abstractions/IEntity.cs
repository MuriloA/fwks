using System;

namespace FwksLabs.Libs.Core.Abstractions;

public interface IEntity : IEntity<Guid>;

public interface IEntity<T> where T : struct
{
    T Id { get; init; }
}
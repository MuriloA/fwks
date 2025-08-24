using System;

namespace FwksLabs.Libs.Core.Abstractions;

public interface IEntity
{
    Guid Id { get; init; }
}
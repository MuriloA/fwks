using System;

namespace FwksLabs.Libs.Core.Abstractions;

public interface ICorrelationIdContext
{
    public Guid CorrelationId { get; }

    public void SetCorrelationId(string correlationId);
}
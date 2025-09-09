using System;
using FwksLabs.Libs.Core.Abstractions;

namespace FwksLabs.Libs.Core.Contexts;

public class CorrelationIdContext : ICorrelationIdContext
{
    public Guid CorrelationId { get; private set; }

    public void SetCorrelationId(string correlationId)
    {
        CorrelationId = Guid.Parse(correlationId);
    }
}
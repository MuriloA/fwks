using System;

namespace FwksLabs.Libs.Core.Contexts;

public class AppRequestContext
{
    public Guid CorrelationId { get; private set; }

    public void SetCorrelationId(string correlationId)
    {
        CorrelationId = Guid.Parse(correlationId);
    }
}
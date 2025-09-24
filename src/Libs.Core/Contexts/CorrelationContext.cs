namespace FwksLabs.Libs.Core.Contexts;

public class CorrelationContext
{
    public string CorrelationId { get; private set; } = string.Empty;
    public string TraceId { get; private set; } = string.Empty;
    public string? SpanId { get; private set; }
    public string? OriginalSpanId { get; private set; }

    public void Update(string correlationId, string traceId, string? spanId)
    {
        CorrelationId = correlationId;
        TraceId = traceId;
        SpanId = spanId;
        OriginalSpanId ??= spanId;
    }

    public void UpdateSpan(string? spanId) => SpanId = spanId;
}
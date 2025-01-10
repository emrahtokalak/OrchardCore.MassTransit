using System;

namespace OrchardCore.MassTransit.Events;

public record RecordCreatedEvent
{
    public string? ContentItemId { get; set; }
    public string? ActivityId { get; set; }
    public string? CustomerId { get; set; }
    public string? CorrelationId { get; set; }
    public DateTime CreatedAt { get; set; }
}
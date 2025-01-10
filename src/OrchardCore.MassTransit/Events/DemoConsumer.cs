using MassTransit;
using Microsoft.Extensions.Logging;

namespace OrchardCore.MassTransit.Events;

public class DemoConsumer : IConsumer<RecordCreatedEvent>
{
    readonly ILogger<DemoConsumer> _logger;

    public DemoConsumer(
        ILogger<DemoConsumer> logger
        )
    {
        _logger = logger;
    }

    /// <summary>
    /// This method is triggered automatically when a message is added to the queue.
    /// You should implement your business logic here.
    /// </summary>
    /// <param name="context">The context containing the <see cref="RecordCreatedEvent"/> message.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Consume(ConsumeContext<RecordCreatedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("RecordCreatedEvent received: {ContentItemId}, {ActivityId}, {CustomerId}, {CorrelationId}, {CreatedAt}",
            message.ContentItemId, message.ActivityId, message.CustomerId, message.CorrelationId, message.CreatedAt);

        // Implement your business logic here.
        await Task.CompletedTask;
    }
}

public class DemoConsumerDefinition : ConsumerDefinition<DemoConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<DemoConsumer> consumerConfigurator, IRegistrationContext context)
    {
        consumerConfigurator.Options<JobOptions<RecordCreatedEvent>>(options => options
            .SetRetry(r => r.Interval(3, TimeSpan.FromSeconds(30)))
            .SetJobTimeout(TimeSpan.FromMinutes(10))
            .SetConcurrentJobLimit(1));
    }
}
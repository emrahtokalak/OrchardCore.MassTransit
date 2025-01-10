using MassTransit;
using Microsoft.Extensions.Logging;

namespace OrchardCore.MessageBrokering.Services;

public class RabbitMQBusPublisher : IRabbitMQBusPublisher
{
    private readonly IPublishEndpoint _publisher;
    private readonly IMessageScheduler _publisherScheduler;
    private readonly ILogger<RabbitMQBusPublisher> _logger;

    public RabbitMQBusPublisher(
        ILogger<RabbitMQBusPublisher> logger,
        IPublishEndpoint publisher,
        IMessageScheduler publisherScheduler)
    {
        _logger = logger;
        _publisher = publisher;
        _publisherScheduler = publisherScheduler;
    }

    public async Task Publish<T>(T message, CancellationToken cancellationToken = default)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        await _publisher.Publish(message, cancellationToken);
    }
}

public interface IRabbitMQBusPublisher
{
    Task Publish<T>(T message, CancellationToken cancellationToken = default);
}
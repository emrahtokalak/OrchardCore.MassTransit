using MassTransit;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Shell.Removing;
using OrchardCore.Modules;

namespace OrchardCore.MessageBrokering;
public class MassTransitEventService : IModularTenantEvents
{
    private readonly IBusControl _bus;
    private readonly ILogger<MassTransitEventService> _logger;

    public MassTransitEventService(IBusControl bus, ILogger<MassTransitEventService> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public Task ActivatedAsync()
    {
        _logger.LogInformation("MassTransit Bus started.");
        return Task.CompletedTask;
    }

    public async Task ActivatingAsync()
    {
        _logger.LogInformation("Starting MassTransit Bus...");
        await _bus.StartAsync();
    }

    public async Task RemovingAsync(ShellRemovingContext context)
    {
        _logger.LogInformation("Stopping MassTransit Bus...");
        await _bus.StopAsync();
    }

    public Task TerminatedAsync()
    {
        _logger.LogInformation("MassTransit Bus stopped.");
        return Task.CompletedTask;
    }

    public async Task TerminatingAsync()
    {
        await _bus.StopAsync();
    }
}
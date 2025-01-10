using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.MassTransit.Events;
using OrchardCore.MessageBrokering;
using OrchardCore.MessageBrokering.Services;
using OrchardCore.Modules;

namespace OrchardCore.MassTransit;

public sealed class Startup : StartupBase
{
    private readonly string _tenant;
    private readonly IShellConfiguration _shellConfiguration;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public Startup(
        ShellSettings shellSettings,
        IShellConfiguration shellConfiguration,
        IConfiguration configuration,
        ILogger<Startup> logger)
    {
        _tenant = shellSettings.Name;
        _shellConfiguration = shellConfiguration;
        _configuration = configuration;
        _logger = logger;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IRabbitMQBusPublisher, RabbitMQBusPublisher>();

        var configuration = _configuration["OrchardCore_MassTransit:RabbitMQ:Configuration"];

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddDelayedMessageScheduler();
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumer<DemoConsumer, DemoConsumerDefinition>();

            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration);
                cfg.UseDelayedMessageScheduler();
                cfg.ConfigureEndpoints(context);
                cfg.Durable = true;
                // cfg.PrefetchCount = 50;
                // cfg.AutoDelete = false;
                // cfg.ConcurrentMessageLimit = 32;
            });
        });

        services.AddSingleton<IModularTenantEvents, MassTransitEventService>();
    }
}


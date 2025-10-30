using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Nix.Messaging;

/// <summary>
/// Расширения для регистрации messaging в DI
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNixMessaging(
        this IServiceCollection services,
        Action<IBusRegistrationConfigurator>? configure = null)
    {
        services.AddMassTransit(x =>
        {
            configure?.Invoke(x);
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IEventBus, MassTransitEventBus>();

        return services;
    }
}










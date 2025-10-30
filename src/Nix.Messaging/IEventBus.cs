namespace Nix.Messaging;

/// <summary>
/// Интерфейс для публикации событий в message broker
/// </summary>
public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
}










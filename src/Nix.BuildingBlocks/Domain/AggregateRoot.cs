using System.ComponentModel.DataAnnotations;

namespace Nix.BuildingBlocks.Domain;

/// <summary>
/// Агрегат-корень. Через него происходят все изменения графа сущностей внутри агрегата.
/// </summary>
public abstract class AggregateRoot<TId> : BaseEntity<TId>
    where TId : notnull
{
    [ConcurrencyCheck] public int Version { get; private set; } = 0;
    private readonly List<DomainEvent> _uncommittedEvents = new();

    /// <summary>Новые события с прошлого коммита.</summary>
    public IReadOnlyCollection<DomainEvent> UncommittedEvents
        => _uncommittedEvents.AsReadOnly();

    /// <summary>
    /// Вызывает AddDomainEvent, а также фиксирует событие как «незакоммиченное».
    /// </summary>
    protected void RaiseDomainEvent(DomainEvent @event)
    {
        base.AddDomainEvent(@event);
        _uncommittedEvents.Add(@event);
        Version++;
    }

    /// <summary>Вызывается UnitOfWork после успешного SaveChanges().</summary>
    internal void MarkEventsAsCommitted() => _uncommittedEvents.Clear();
}

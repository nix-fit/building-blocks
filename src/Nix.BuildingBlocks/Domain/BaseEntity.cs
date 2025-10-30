namespace Nix.BuildingBlocks.Domain;

/// <summary>
/// Áàçîâûé êëàññ äëÿ âñåõ ñóùíîñòåé (Entity) -- õğàíèò Id è ëîãèêó ñğàâíåíèÿ.
/// </summary>
public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; } = default!;

    // —————————————  ÑÎÁÛÒÈß ÄÎÌÅÍÀ  —————————————
    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent @event) => _domainEvents.Add(@event);

    public void ClearDomainEvents() => _domainEvents.Clear();

    // —————————————  Equality  —————————————
    public override bool Equals(object? obj) => Equals(obj as BaseEntity<TId>);
    public bool Equals(BaseEntity<TId>? other)
        => other is not null && EqualityComparer<TId>.Default.Equals(Id, other.Id);

    public override int GetHashCode() => Id!.GetHashCode();

    public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right)
        => Equals(left, right);

    public static bool operator !=(BaseEntity<TId>? left, BaseEntity<TId>? right)
        => !(left == right);
}

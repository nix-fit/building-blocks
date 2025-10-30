using Nix.BuildingBlocks.Domain;

namespace Nix.EnrollmentService.Contracts.Events;

/// <summary>
/// Событие выдачи доступа к ресурсу
/// </summary>
public sealed class AccessGrantedEvent : DomainEvent
{
    public Guid TenantId { get; }
    public Guid UserId { get; }
    public string ResourceType { get; }
    public Guid ResourceId { get; }
    public IReadOnlyList<string> Actions { get; }
    public string Source { get; }
    public DateTimeOffset? ExpiresAt { get; }

    public AccessGrantedEvent(
        Guid tenantId,
        Guid userId,
        string resourceType,
        Guid resourceId,
        IReadOnlyList<string> actions,
        string source,
        DateTimeOffset? expiresAt = null)
    {
        TenantId = tenantId;
        UserId = userId;
        ResourceType = resourceType;
        ResourceId = resourceId;
        Actions = actions;
        Source = source;
        ExpiresAt = expiresAt;
    }
}

/// <summary>
/// Событие отзыва доступа к ресурсу
/// </summary>
public sealed class AccessRevokedEvent : DomainEvent
{
    public Guid TenantId { get; }
    public Guid UserId { get; }
    public string ResourceType { get; }
    public Guid ResourceId { get; }
    public IReadOnlyList<string> Actions { get; }
    public string Reason { get; }

    public AccessRevokedEvent(
        Guid tenantId,
        Guid userId,
        string resourceType,
        Guid resourceId,
        IReadOnlyList<string> actions,
        string reason)
    {
        TenantId = tenantId;
        UserId = userId;
        ResourceType = resourceType;
        ResourceId = resourceId;
        Actions = actions;
        Reason = reason;
    }
}





















































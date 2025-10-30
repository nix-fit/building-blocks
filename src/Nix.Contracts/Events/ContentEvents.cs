using Nix.BuildingBlocks.Domain;

namespace Nix.EnrollmentService.Contracts.Events;

/// <summary>
/// Событие: контент удалён из ресурса (курс, марафон и т.д.)
/// Публикуется CourseService → подписывается EnrollmentService
/// </summary>
public sealed class ContentDeletedEvent : DomainEvent
{
    public Guid ContentId { get; }
    public string ContentType { get; }
    public Guid ResourceId { get; }
    public Guid TenantId { get; }

    public ContentDeletedEvent(
        Guid contentId,
        string contentType,
        Guid resourceId,
        Guid tenantId)
    {
        ContentId = contentId;
        ContentType = contentType;
        ResourceId = resourceId;
        TenantId = tenantId;
    }
}

/// <summary>
/// Событие: порядок контента изменён
/// Публикуется CourseService → подписывается EnrollmentService
/// </summary>
public sealed class ContentReorderedEvent : DomainEvent
{
    public Guid ResourceId { get; }
    public string ResourceType { get; }
    public Guid TenantId { get; }
    public List<ContentOrderChange> Changes { get; }

    public ContentReorderedEvent(
        Guid resourceId,
        string resourceType,
        Guid tenantId,
        List<ContentOrderChange> changes)
    {
        ResourceId = resourceId;
        ResourceType = resourceType;
        TenantId = tenantId;
        Changes = changes;
    }
}

/// <summary>
/// Изменение порядка одного контента
/// </summary>
public sealed record ContentOrderChange(
    Guid ContentId,
    int NewOrder
);
















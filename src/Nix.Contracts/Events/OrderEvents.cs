using Nix.BuildingBlocks.Domain;

namespace Nix.EnrollmentService.Contracts.Events;

/// <summary>
/// Событие оплаты заказа (входящее)
/// </summary>
public sealed class OrderPaidEvent : DomainEvent
{
    public Guid TenantId { get; }
    public Guid OrderId { get; }
    public Guid UserId { get; }
    public Guid CourseId { get; }
    public int TierOrder { get; }
    public int? DurationDays { get; }

    public OrderPaidEvent(
        Guid tenantId,
        Guid orderId,
        Guid userId,
        Guid courseId,
        int tierOrder,
        int? durationDays = null)
    {
        TenantId = tenantId;
        OrderId = orderId;
        UserId = userId;
        CourseId = courseId;
        TierOrder = tierOrder;
        DurationDays = durationDays;
    }
}

/// <summary>
/// Событие возврата заказа (входящее)
/// </summary>
public sealed class OrderRefundedEvent : DomainEvent
{
    public Guid TenantId { get; }
    public Guid OrderId { get; }
    public Guid UserId { get; }
    public Guid CourseId { get; }

    public OrderRefundedEvent(
        Guid tenantId,
        Guid orderId,
        Guid userId,
        Guid courseId)
    {
        TenantId = tenantId;
        OrderId = orderId;
        UserId = userId;
        CourseId = courseId;
    }
}

/// <summary>
/// Событие активации подписки (входящее, опционально)
/// </summary>
public sealed class SubscriptionActivatedEvent : DomainEvent
{
    public Guid TenantId { get; }
    public Guid SubscriptionId { get; }
    public Guid UserId { get; }
    public Guid CourseId { get; }
    public int TierOrder { get; }
    public DateTimeOffset ExpiresAt { get; }

    public SubscriptionActivatedEvent(
        Guid tenantId,
        Guid subscriptionId,
        Guid userId,
        Guid courseId,
        int tierOrder,
        DateTimeOffset expiresAt)
    {
        TenantId = tenantId;
        SubscriptionId = subscriptionId;
        UserId = userId;
        CourseId = courseId;
        TierOrder = tierOrder;
        ExpiresAt = expiresAt;
    }
}

/// <summary>
/// Событие истечения подписки (входящее, опционально)
/// </summary>
public sealed class SubscriptionExpiredEvent : DomainEvent
{
    public Guid TenantId { get; }
    public Guid SubscriptionId { get; }
    public Guid UserId { get; }
    public Guid CourseId { get; }

    public SubscriptionExpiredEvent(
        Guid tenantId,
        Guid subscriptionId,
        Guid userId,
        Guid courseId)
    {
        TenantId = tenantId;
        SubscriptionId = subscriptionId;
        UserId = userId;
        CourseId = courseId;
    }
}






























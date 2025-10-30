using Nix.EnrollmentService.Domain.Entities;

namespace Nix.EnrollmentService.Contracts.Dtos;

/// <summary>
/// Запрос на создание расписания контента
/// </summary>
public sealed record CreateContentScheduleRequest(
    Guid TenantId,
    Guid ResourceId,
    string ResourceType,
    ScheduleType ScheduleType,
    StartMode StartMode,
    DateTimeOffset? StartDate,
    Guid CreatedBy,
    List<ContentScheduleItemRequest>? Items
);

/// <summary>
/// Запрос на обновление расписания
/// </summary>
public sealed record UpdateContentScheduleRequest(
    ScheduleType ScheduleType,
    StartMode StartMode,
    DateTimeOffset? StartDate
);

/// <summary>
/// Данные для создания item расписания
/// </summary>
public sealed record ContentScheduleItemRequest(
    Guid ContentId,
    string ContentType,
    int ContentOrder,
    UnlockType UnlockType,
    int? UnlockDelayDays,
    int? UnlockDelayHours,
    DateTimeOffset? UnlockAt,
    Guid? RequiredContentId,
    string? RequiredEvent
);

/// <summary>
/// Запрос на bulk добавление items
/// </summary>
public sealed record BulkAddScheduleItemsRequest(
    List<ContentScheduleItemRequest> Items
);

/// <summary>
/// Ответ с информацией о расписании
/// </summary>
public sealed record ContentScheduleResponse(
    Guid Id,
    Guid TenantId,
    Guid ResourceId,
    string ResourceType,
    ScheduleType ScheduleType,
    StartMode StartMode,
    DateTimeOffset? StartDate,
    bool IsEnabled,
    Guid CreatedBy,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    List<ContentScheduleItemResponse> Items
);

/// <summary>
/// Информация об item расписания
/// </summary>
public sealed record ContentScheduleItemResponse(
    Guid Id,
    Guid ContentId,
    string ContentType,
    int ContentOrder,
    UnlockType UnlockType,
    int? UnlockDelayDays,
    int? UnlockDelayHours,
    DateTimeOffset? UnlockAt,
    Guid? RequiredContentId,
    string? RequiredEvent,
    bool IsDeleted,
    DateTimeOffset? DeletedAt
);

/// <summary>
/// Ответ на проверку разблокировки контента
/// </summary>
public sealed record CheckUnlockResponse(
    bool IsUnlocked,
    bool CanAccess,
    string Reason,
    DateTimeOffset? UnlockDate,
    string? UnlockIn,
    ScheduleInfoDto? ScheduleInfo,
    AccessInfoDto? AccessInfo,
    BypassInfoDto? Bypass
);

/// <summary>
/// Информация о расписании в check-unlock
/// </summary>
public sealed record ScheduleInfoDto(
    bool ScheduleExists,
    ScheduleType? ScheduleType,
    StartMode? StartMode,
    bool IsEnabled
);

/// <summary>
/// Информация о доступе в check-unlock
/// </summary>
public sealed record AccessInfoDto(
    bool HasResourceAccess,
    int? TierOrder,
    DateTimeOffset? AccessGrantedAt,
    bool IgnoreSchedule
);

/// <summary>
/// Информация о bypass в check-unlock
/// </summary>
public sealed record BypassInfoDto(
    bool IsOwner,
    bool IsAdmin,
    bool IsPreview
);
















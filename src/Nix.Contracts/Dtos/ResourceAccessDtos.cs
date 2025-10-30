using Nix.EnrollmentService.Domain.Entities;

namespace Nix.EnrollmentService.Contracts.Dtos;

/// <summary>
/// Запрос на предоставление доступа к ресурсу
/// </summary>
public sealed record GrantResourceAccessRequest(
    Guid UserId,
    Guid ResourceId,
    string ResourceType,
    int TierOrder,
    DateTimeOffset? ExpiresAt,
    AccessGrantReason GrantReason,
    string? GrantNote,
    AccessRole? Role = null                     // По умолчанию Student
);

/// <summary>
/// Запрос на продление доступа
/// </summary>
public sealed record ExtendResourceAccessRequest(
    DateTimeOffset? NewExpiresAt
);

/// <summary>
/// Запрос на улучшение уровня доступа
/// </summary>
public sealed record UpgradeTierRequest(
    int NewTierOrder
);

/// <summary>
/// Ответ с информацией о доступе
/// </summary>
public sealed record ResourceAccessResponse(
    Guid Id,
    Guid UserId,
    Guid ResourceId,
    string ResourceType,
    Guid TenantId,
    int TierOrder,
    DateTimeOffset AccessGrantedAt,
    DateTimeOffset? AccessExpiresAt,
    bool IsTemporary,
    bool IsActive,
    bool IsExpired,
    AccessGrantReason GrantReason,
    string? GrantNote,
    AccessRole Role                             // Роль пользователя
);

/// <summary>
/// Ответ на проверку доступа
/// </summary>
public sealed record CheckResourceAccessResponse(
    bool HasAccess,
    int? TierOrder,
    IReadOnlyList<string> Permissions,
    bool IsTemporary,
    DateTimeOffset? ExpiresAt,
    string? Reason,
    AccessRole? Role = null                     // Роль пользователя
);

/// <summary>
/// Запрос на создание/обновление админской роли
/// </summary>
public sealed record GrantAdminRoleRequest(
    Guid UserId,
    Guid ResourceId,
    string ResourceType,
    AccessRole Role,
    List<string> Permissions
);

/// <summary>
/// Информация об админской роли
/// </summary>
public sealed record AdminRoleResponse(
    Guid Id,
    Guid UserId,
    Guid ResourceId,
    string ResourceType,
    Guid TenantId,
    AccessRole Role,
    List<string> Permissions,
    Guid GrantedBy,
    DateTimeOffset GrantedAt
);







